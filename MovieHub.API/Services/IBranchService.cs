using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IBranchService
    {
        Task<BranchReadDto> CreateBranchAsync(BranchCreateDto branchCreateDto);
        Task<BranchReadDto?> GetBranchByIdAsync(int id);
        Task DeactivateBranchByIdAsync(int id);
        Task UpdateBranchByIdAsync(int id, BranchUpdateDto branchUpdateDto);
        Task<IEnumerable<BranchReadDto>> GetAllBranchesAsync();
        Task ResetBranchManagerByIdAsync(int id);
    }
}
