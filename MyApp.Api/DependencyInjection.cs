using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyApp.Application;
using MyApp.Infrastructure;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
                    .AddInfrastructureDI(configuration);

            services.AddHttpContextAccessor();
            

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:7012")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .SetIsOriginAllowed(origin => true);
                });
            });

            

            


           
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        
                        var token = context.HttpContext.Request.Cookies["jwt_token"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"])
                    )
                };
            });

            return services;
        }
    }
}
