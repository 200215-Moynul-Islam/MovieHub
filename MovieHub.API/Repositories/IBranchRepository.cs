using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public interface IBranchRepository : INamedRepository<Branch>
    {
        #region Existence Checks
        Task<bool> IsManagerAssignedAsync(Guid managerId);
        #endregion
    }
}