using MovieHub.API.Models.Base;

namespace MovieHub.API.Repositories.Base
{
    public interface ISoftDeletableRepositoryBase<T> : IRepositoryBase<T>
        where T : SoftDeletableEntityBase
    {
        Task DeactivateByIdAsync(int id);
    }
}
