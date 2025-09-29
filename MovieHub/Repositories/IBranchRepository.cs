using MovieHub.API.DTOs;

namespace MovieHub.API.Repositories
{
    public interface IBranchRepository
    {

        #region CRUD Operations
        #region Create
        Task<BranchReadDto> CreateBranchAsync(BranchCreateDto branchCreateDto);
        #endregion
        #region Read
        Task<BranchReadDto?> GetBranchByIdAsync(int id);
        #endregion
        #endregion

        #region Existence Checks
        Task<bool> BranchNameExistsAsync(string branchName);
        Task<bool> IsManagerAssignedAsync(Guid managerId);
        #endregion
    }
}