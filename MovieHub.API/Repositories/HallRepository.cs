using Microsoft.EntityFrameworkCore;
using MovieHub.API.Constants;
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

        public async Task<IEnumerable<Hall>> GetHallsByBranchIdAsync(
            int branchId,
            int offset,
            int limit
        )
        {
            return await _dbSet
                .Where(h => h.IsDeleted == false && h.BranchId == branchId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Guid?> GetManagerIdByIdAsync(int id)
        {
            return await _dbContext
                .Halls.Where(h =>
                    h.IsDeleted == false
                    && h.Branch.IsDeleted == false
                    && h.Id == id
                )
                .Select(h => h.Branch.ManagerId)
                .FirstOrDefaultAsync();
        }
    }
}
