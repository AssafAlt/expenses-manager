using MyApp.Application.DTOs.Auth;
using MyApp.Application.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<ServiceResponse<object>> LoginAsync(LoginUserDto loginUserDto);
        public Task<ServiceResponse<object>> RegisterAsync(RegisterUserDto registerUserDto);
    }
}
