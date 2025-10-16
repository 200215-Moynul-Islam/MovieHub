using MovieHub.API.Models.Base;

namespace MovieHub.API.Repositories
{
    public interface INamedRepository<T> : IRepository<T>
        where T : SoftDeletableNamedEntityBase
    {
        Task<bool> NameExistsCaseInsensitiveAsync(string name);
    }
}
