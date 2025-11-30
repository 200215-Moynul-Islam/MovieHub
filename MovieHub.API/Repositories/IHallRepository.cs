using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public interface IHallRepository : INamedSoftDeletableRepositoryBase<Hall>
    {
        Task DeactivateHallsByBranchIdAsync(int branchId);
        Task<bool> HallNameExistsForBranchCaseInsensitiveAsync(
            string name,
            int branchId
        );
        Task<IEnumerable<Hall>> GetHallsByBranchIdAsync(
            int branchId,
            int offset,
            int limit
        );
        Task<Guid?> GetManagerIdByIdAsync(int id);
    }
}
