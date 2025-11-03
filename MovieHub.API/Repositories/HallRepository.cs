using Microsoft.EntityFrameworkCore;
using MovieHub.API.Constants;
using MovieHub.API.Data;
using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public class HallRepositoriy
        : SoftDeletableNamedRepository<Hall>,
            IHallRepository
    {
        public HallRepositoriy(MovieHubDbContext dbContext)
            : base(dbContext) { }

        public async Task DeactivateHallsByBranchIdAsync(int branchId)
        {
            await _dbContext
                .Halls.Where(h => h.BranchId == branchId)
                .ExecuteUpdateAsync(s => s.SetProperty(h => h.IsDeleted, true));
        }

        public async Task<bool> HallNameExistsForBranchCaseInsensitiveAsync(
            string name,
            int branchId
        )
        {
            return await _dbSet.AnyAsync(e =>
                e.IsDeleted == false
                && e.BranchId == branchId
                && EF.Functions.Collate(
                    e.Name,
                    DatabaseCollations.CaseInsensitive
                ) == name
            );
        }
    }
}
