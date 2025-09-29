using MovieHub.API.DTOs;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        #region Create
        public async Task<BranchReadDto> CreateBranchAsync(BranchCreateDto branchCreateDto)
        {
            // ToDo: Validate if manager exists in User module when it's done
            var branchNameExists = _branchRepository.BranchNameExistsAsync(branchCreateDto.Name);
            var isManagerAssigned = branchCreateDto.ManagerId == null 
                ? Task.FromResult(false) 
                : _branchRepository.IsManagerAssignedAsync(branchCreateDto.ManagerId.Value);
            // Wait for both tasks to be completed
            await Task.WhenAll(branchNameExists, isManagerAssigned);
            
            if (branchNameExists.Result)
            {
                throw new Exception($"Branch with name '{branchCreateDto.Name}' already exists."); // Use specific exception.
            }

            if (isManagerAssigned.Result)
            {
                throw new Exception($"Manager with ID '{branchCreateDto.ManagerId}' is already assigned to another branch."); // Use specific exception.
            }

            return await _branchRepository.CreateBranchAsync(branchCreateDto);
        }
        #endregion

        #region Read
        public async Task<BranchReadDto?> GetBranchByIdAsync(int id)
        {
            return await _branchRepository.GetBranchByIdAsync(id);
        }
        #endregion
    }
}