using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IBranchService
    {
        Task<BranchReadDto?> GetBranchByIdAsync(int id);
        Task<IEnumerable<BranchReadDto>> GetAllBranchesAsync(
            int offset,
            int limit
        );
        Task ResetBranchManagerByIdAsync(int id);
    }
}
