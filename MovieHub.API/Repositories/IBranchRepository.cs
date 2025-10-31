using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public interface IBranchRepository : ISoftDeletableNamedRepository<Branch>
    {
        Task<bool> IsManagerAvailableAsync(Guid managerId);
        Task ResetBranchManagerByIdAsync(int id);
    }
}
