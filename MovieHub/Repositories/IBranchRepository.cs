using MovieHub.API.Models;
using MovieHub.Repositories;

namespace MovieHub.API.Repositories
{
    public interface IBranchRepository : IRepository<Branch>
    {
        #region Existence Checks
        Task<bool> BranchNameExistsAsync(string branchName);
        Task<bool> IsManagerAssignedAsync(Guid managerId);
        #endregion
    }
}