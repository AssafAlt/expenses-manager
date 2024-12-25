
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.Auth;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Responses;



namespace MyApp.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var results = await _authService.RegisterAsync(registerUserDto);
            return StatusCode(results.StatusCode, results.Data);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            
            var results = await _authService.LoginAsync(loginUserDto);

            if (results.StatusCode == StatusCodes.Status401Unauthorized) return StatusCode(results.StatusCode, results.Data);
            var tokenResponse = (TokenResponse)results.Data;  
            



            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Path = "/",
                Expires = DateTime.Now.AddMinutes(30)
            };
            Response.Cookies.Append("jwt_token", tokenResponse.Token, cookieOptions);
           


            return StatusCode(results.StatusCode, "Logined successfully");

        }

    }
}
