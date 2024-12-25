using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyApp.Application.Interfaces.Services;
using MyApp.Infrastructure.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApp.Infrastructure.Implementations.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _configuration;
        

        public TokenService(IOptions<JwtSettings> jwtSettings, IConfiguration configuration)
        {

            _jwtSettings = jwtSettings.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
            _configuration = configuration;
            

        }
        public string CreateToken(AppUser user)
        {

           
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
               
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = creds,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string? GetEmailFromClaims(string token)
        {
            var emailClaimType = _configuration["JWT:EmailClaimType"];
           
            var email = GetClaimFromToken(token, emailClaimType);
            return email;
        }

        public List<Claim> ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var signingKey = _configuration["JWT:SigningKey"];
                var key = Encoding.UTF8.GetBytes(signingKey);

                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                };
                var principal = tokenHandler.ValidateToken(token, parameters, out _);

                return principal?.Claims.ToList();
            }
            catch
            {
                return null;
            }
        }

        private string? GetClaimFromToken(string token, string claimType)
        {
            var claims = ValidateToken(token);        

            return claims?.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
    }
}
