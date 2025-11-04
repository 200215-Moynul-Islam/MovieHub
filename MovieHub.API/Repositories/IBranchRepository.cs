using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public interface IBranchRepository
        : INamedSoftDeletableRepositoryBase<Branch>
    {
        Task<bool> IsManagerAvailableAsync(Guid managerId);
        Task ResetBranchManagerByIdAsync(int id);
    }
}
