using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public class HallRepositoriy : SoftDeletableNamedRepository<Hall>, IHallRepository
    {
        public HallRepositoriy(MovieHubDbContext dbContext)
            : base(dbContext) { }

        public async Task DeactivateByBranchIdAsync(int branchId)
        {
            await _dbContext
                .Halls.Where(h => h.BranchId == branchId)
                .ExecuteUpdateAsync(s => s.SetProperty(h => h.IsDeleted, true));
        }
    }
}
