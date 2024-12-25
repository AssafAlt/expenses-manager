using MyApp.Domain.Entities;
namespace MyApp.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByEmailAsync(string email);
        Task CreateUserAsync(AppUser user);
    }
}
