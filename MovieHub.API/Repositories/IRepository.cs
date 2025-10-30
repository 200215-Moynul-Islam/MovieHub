using MovieHub.API.Models.Base;

namespace MovieHub.API.Repositories
{
    public interface IRepository<T>
        where T : EntityBase
    {
        Task CreateAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task SaveChangesAsync();
        Task ExecuteInTransactionAsync(Func<Task> operation);
        Task<bool> ExistsByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
