using MovieHub.API.Models.Base;

namespace MovieHub.API.Repositories.Base
{
    public interface INamedSoftDeletableRepositoryBase<T>
        : ISoftDeletableRepositoryBase<T>
        where T : NamedSoftDeletableEntityBase
    {
        Task<bool> NameExistsCaseInsensitiveAsync(string name);
    }
}
