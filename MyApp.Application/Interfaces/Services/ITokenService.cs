using MyApp.Domain.Entities;
using System.Security.Claims;


namespace MyApp.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        List<Claim> ValidateToken(string token);
        string? GetEmailFromClaims(string token);
       

    }
}
