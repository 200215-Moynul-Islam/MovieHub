using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public class BranchRepository : NamedRepository<Branch>, IBranchRepository
    {
        public BranchRepository(MovieHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsManagerAssignedAsync(Guid managerId)
        {
            return await _dbContext.Branches
                .AnyAsync(b => b.ManagerId == managerId);
        }
    }
}
