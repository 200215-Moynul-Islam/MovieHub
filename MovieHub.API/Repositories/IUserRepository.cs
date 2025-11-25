using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task SaveChangesAsync();
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
    }
}
