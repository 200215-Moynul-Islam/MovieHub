using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public class HallRepositoriy
        : NamedSoftDeletableRepositoryBase<Hall>,
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
    }
}
