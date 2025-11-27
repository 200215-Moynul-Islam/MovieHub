using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieHubDbContext _dbContext;

        public UserRepository(MovieHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
            return;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _dbContext.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserWithRolesByEmailOrUsernameAsync(
            string emailOrUsername
        )
        {
            return await _dbContext
                .Users.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u =>
                    u.IsDeleted == false
                    && (
                        u.Email == emailOrUsername
                        || u.Username == emailOrUsername
                    )
                );
        }
    }
}
