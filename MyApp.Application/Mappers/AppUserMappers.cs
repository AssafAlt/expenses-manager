using MyApp.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Mappers
{
    public static class AppUserMappers
    {
        public static AppUser ToAppUserFromRegisterDto(this RegisterUserDto registerUserDto,string Password)
        {
            return new AppUser
            {
                Email = registerUserDto.Email,
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                PhoneNumber = registerUserDto.PhoneNumber,
                Password = Password
            };
        }
    }
}
