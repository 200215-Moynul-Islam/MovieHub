using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Models.Base;

namespace MovieHub.API.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly MovieHubDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(MovieHubDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            IQueryable<T> query = _dbSet;
            var isDeletedProperty = typeof(T).GetProperty(
                nameof(SoftDeletableEntityBase.IsDeleted)
            );
            if (isDeletedProperty != null && isDeletedProperty.PropertyType == typeof(bool))
            {
                // Filter out soft-deleted records if IsDeleted property exists
                query = query.Where(e =>
                    EF.Property<bool>(e, nameof(SoftDeletableEntityBase.IsDeleted)) == false
                );
            }
            // Filter by Id property
            return await query.FirstOrDefaultAsync(e =>
                EF.Property<int>(e, nameof(EntityBase.Id)) == id
            );
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            return;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            return;
        }

        public async Task SaveChangesAync()
        {
            await _dbContext.SaveChangesAsync();
            return;
        }
    }
}
