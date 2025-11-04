using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Models.Base;

namespace MovieHub.API.Repositories.Base
{
    public abstract class SoftDeletableRepositoryBase<T>
        : RepositoryBase<T>,
            ISoftDeletableRepositoryBase<T>
        where T : SoftDeletableEntityBase
    {
        public SoftDeletableRepositoryBase(MovieHubDbContext dbContext)
            : base(dbContext) { }

        public override async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(e =>
                e.Id == id && e.IsDeleted == false
            );
        }

        public override async Task<bool> ExistsByIdAsync(int id)
        {
            return await _dbSet.AnyAsync(e =>
                e.Id == id && e.IsDeleted == false
            );
        }

        public async Task DeactivateByIdAsync(int id)
        {
            await _dbSet
                .Where(e => e.Id == id)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.IsDeleted, true));
        }

        public override async Task<IEnumerable<T>> GetAllAsync(
            int offset,
            int limit
        )
        {
            return await _dbSet
                .Where(e => e.IsDeleted == false)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }
    }
}
