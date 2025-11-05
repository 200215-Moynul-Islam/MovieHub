using MovieHub.API.DTOs;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public interface IBranchHallService
    {
        Task DeactivateBranchWithHallsByBranchIdAsync(int branchId);
        Task<IEnumerable<HallReadDto>> GetHallsByBranchIdAsync(
            int branchId,
            int offset,
            int limit
        );
    }
}
