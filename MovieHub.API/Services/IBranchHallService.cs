using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public interface IBranchHallService
    {
        Task DeactivateBranchWithHallsByBranchIdAsync(int branchId);
    }
}
