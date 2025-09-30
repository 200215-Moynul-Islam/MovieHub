using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IBranchService
    {
        #region Create
        Task<BranchReadDto> CreateBranchAsync(BranchCreateDto branchCreateDto);
        #endregion

        #region Read
        Task<BranchReadDto?> GetBranchByIdAsync(int id);
        #endregion
    }
}