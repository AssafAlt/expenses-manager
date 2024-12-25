using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.DTOs.Auth;
using MyApp.Application.Interfaces.Repositories;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Interfaces.Services.Base;
using MyApp.Application.Mappers;
using MyApp.Application.Responses;
using MyApp.Application.Responses.Base;


namespace MyApp.Infrastructure.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepository;
        private readonly IBaseService _baseService;
        public AuthService(ITokenService tokenService, IPasswordService passwordService, IUserRepository userRepository,IBaseService baseService)
        {
            _tokenService = tokenService;
            _passwordService = passwordService;
            _userRepository = userRepository;
            _baseService = baseService;
        }

        public async Task<ServiceResponse<object>> LoginAsync(LoginUserDto loginUserDto)
        {
            return await _baseService.HandleServiceOperationAsync<object>(async () =>
            {

                var user = await _userRepository.GetUserByEmailAsync(loginUserDto.Email);
                if (user == null || !_passwordService.VerifyPassword(loginUserDto.Password, user.Password))
                {
                    return new ServiceResponse<object>(StatusCodes.Status401Unauthorized, "Invalid username or password");
                }
                var tokenResponse = new TokenResponse
                {
                    Token = _tokenService.CreateToken(user)
                };
                return new ServiceResponse<object>(StatusCodes.Status200OK, tokenResponse);

            });
            
        }
       

        public async Task<ServiceResponse<object>> RegisterAsync(RegisterUserDto registerUserDto)
        {
            return await _baseService.HandleServiceOperationAsync<object>(async () =>
            {
                var existingUser = await _userRepository.GetUserByEmailAsync(registerUserDto.Email);
                if (existingUser != null)
                {
                    return new ServiceResponse<object>(StatusCodes.Status400BadRequest, "User already exists");
                }


                var newUser = registerUserDto.ToAppUserFromRegisterDto(_passwordService.HashPassword(registerUserDto.Password));

                await _userRepository.CreateUserAsync(newUser);

                return new ServiceResponse<object>(StatusCodes.Status201Created, "User was created successfully");

            });
          

        }
    }
}

