using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public class BranchRepository
        : NamedSoftDeletableRepositoryBase<Branch>,
            IBranchRepository
    {
        public BranchRepository(MovieHubDbContext dbContext)
            : base(dbContext) { }

        // TODO: Move this method to UserRepository.
        public async Task<bool> IsManagerAvailableAsync(Guid managerId)
        {
            return !await _dbContext.Branches.AnyAsync(b =>
                b.ManagerId == managerId
            );
        }

        public async Task ResetBranchManagerByIdAsync(int id)
        {
            await _dbContext
                .Branches.Where(b => b.Id == id)
                .ExecuteUpdateAsync(s =>
                    s.SetProperty(b => b.ManagerId, (Guid?)null)
                );
        }
    }
}
