using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Models.Base;

namespace MovieHub.API.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : EntityBase
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

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
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

        public async Task ExecuteInTransactionAsync(Func<Task> operation)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            await operation();
            await transaction.CommitAsync();
        }

        public virtual async Task<bool> ExistsByIdAsync(int id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
