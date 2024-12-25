using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces.Repositories;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Interfaces.Services.Base;
using MyApp.Infrastructure.Auth;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Implementations.Repositories;
using MyApp.Infrastructure.Implementations.Services;
using MyApp.Infrastructure.Implementations.Services.Base;


namespace MyApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.Configure<JwtSettings>(configuration.GetSection("JWT"));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IBaseService, BaseService>();

            return services;
        }
    }
}
