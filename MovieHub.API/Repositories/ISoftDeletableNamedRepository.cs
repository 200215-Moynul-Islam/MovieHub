using MovieHub.API.Models.Base;

namespace MovieHub.API.Repositories
{
    public interface ISoftDeletableNamedRepository<T> : IRepository<T>
        where T : SoftDeletableNamedEntityBase
    {
        Task<bool> NameExistsCaseInsensitiveAsync(string name);
        Task DeactivateByIdAsync(int id);
    }
}
