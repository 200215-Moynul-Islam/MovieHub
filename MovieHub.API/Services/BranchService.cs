using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly IHallService _hallService;

        public BranchService(
            IBranchRepository branchRepository,
            IMapper mapper,
            IHallService hallService
        )
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
            _hallService = hallService;
        }

        public async Task<BranchReadDto> CreateBranchAsync(BranchCreateDto branchCreateDto)
        {
            await EnsureBranchNameIsUniqueAsync(branchCreateDto.Name);
            await EnsureManagerExistsAndAvailableByIdAsync(branchCreateDto.ManagerId);

            var branch = _mapper.Map<Branch>(branchCreateDto);
            await _branchRepository.CreateAsync(branch);
            await _branchRepository.SaveChangesAync();
            return _mapper.Map<BranchReadDto>(branch);
        }

        public async Task<BranchReadDto?> GetBranchByIdAsync(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            return _mapper.Map<BranchReadDto>(branch);
        }

        public async Task DeactivateBranchByIdAsync(int id)
        {
            await EnsureBranchExistsByIdAsync(id);

            await _branchRepository.ExecuteInTransactionAsync(async () =>
            {
                Task t1 = _branchRepository.DeactivateByIdAsync(id);
                Task t2 = _hallService.DeactivateByBranchIdAsync(id);
                await Task.WhenAll(t1, t2);
            });
        }

        public async Task UpdateBranchByIdAsync(int id, BranchUpdateDto branchUpdateDto)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            EnsureBranchExists(branch, id);

            if (branchUpdateDto.Name is not null)
            {
                await EnsureBranchNameIsUniqueAsync(branchUpdateDto.Name);
            }
            if (branchUpdateDto.ManagerId is not null)
            {
                await EnsureManagerExistsAndAvailableByIdAsync(branchUpdateDto.ManagerId);
            }

            _mapper.Map(branchUpdateDto, branch);

            await _branchRepository.SaveChangesAync();
            return;
        }

        #region Private Methods
        private async Task EnsureBranchExistsByIdAsync(int id)
        {
            bool exists = await _branchRepository.ExistsByIdAsync(id);
            if (!exists)
            {
                throw new Exception($"Branch with id '{id}' does not exist.");
            }
        }

        private void EnsureBranchExists(Branch? branch, int id)
        {
            if (branch is null)
            {
                throw new Exception($"Branch with id '{id}' does not exists.");
            }
        }

        private async Task EnsureBranchNameIsUniqueAsync(string name)
        {
            if (await _branchRepository.NameExistsCaseInsensitiveAsync(name))
            {
                throw new Exception($"Branch with name '{name}' already exists."); // Use specific exception and avoid magic exception message.
            }
        }

        private async Task EnsureManagerExistsAndAvailableByIdAsync(Guid? managerId)
        {
            if (managerId is null)
            {
                return; // Null manager ID is always valid since a branch can exist without a manager.
            }
            // TODO: Validate if manager exists in User table when User table is created and throw exception if not found
            else if (await _branchRepository.IsManagerAssignedAsync(managerId.Value))
            {
                throw new Exception(
                    $"Manager with ID '{managerId}' is already assigned to another branch."
                ); // Use specific exception and avoid magic exception message.
            }
        }
        #endregion
    }
}
