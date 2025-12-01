using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IUserBranchService
    {
        Task<BranchReadDto> CreateBranchAsync(BranchCreateDto branchCreateDto);
        Task<BranchReadDto> UpdateBranchByIdAsync(
            int id,
            BranchUpdateDto branchUpdateDto
        );
    }
}
