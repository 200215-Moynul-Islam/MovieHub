using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class UserBranchService : IUserBranchService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;

        public UserBranchService(
            IMapper mapper,
            IUserRepository userRepository,
            IBranchRepository branchRepository
        )
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _branchRepository = branchRepository;
        }

        public async Task<BranchReadDto> CreateBranchAsync(
            BranchCreateDto branchCreateDto
        )
        {
            await EnsureBranchNameIsUniqueOrThrowAsync(branchCreateDto.Name!);
            await EnsureManagerExistsAndAvailableByIdOrThrowAsync(
                branchCreateDto.ManagerId
            );

            var branch = _mapper.Map<Branch>(branchCreateDto);
            await _branchRepository.CreateAsync(branch);
            await _branchRepository.SaveChangesAsync();
            return _mapper.Map<BranchReadDto>(branch);
        }

        public async Task<BranchReadDto> UpdateBranchByIdAsync(
            int id,
            BranchUpdateDto branchUpdateDto
        )
        {
            var branch = await GetBranchByIdOrThrowAsync(id);

            if (branchUpdateDto.Name is not null)
            {
                await EnsureBranchNameIsUniqueOrThrowAsync(
                    branchUpdateDto.Name
                );
            }
            if (branchUpdateDto.ManagerId is not null)
            {
                await EnsureManagerExistsAndAvailableByIdOrThrowAsync(
                    branchUpdateDto.ManagerId
                );
            }

            _mapper.Map(branchUpdateDto, branch);

            await _branchRepository.SaveChangesAsync();
            return _mapper.Map<BranchReadDto>(branch);
        }

        #region Private Methods
        private async Task<Branch> GetBranchByIdOrThrowAsync(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch is null)
            {
                throw new Exception($"Branch with id '{id}' does not exists.");
            }
            return branch;
        }

        private async Task EnsureBranchNameIsUniqueOrThrowAsync(string name)
        {
            if (await _branchRepository.NameExistsCaseInsensitiveAsync(name))
            {
                throw new Exception(
                    $"Branch with name '{name}' already exists."
                ); // Use specific exception and avoid magic exception message.
            }
        }

        private async Task EnsureManagerExistsAndAvailableByIdOrThrowAsync(
            Guid? managerId
        )
        {
            if (managerId is null)
            {
                return; // Null manager ID is always valid since a branch can exist without a manager.
            }

            if (
                !await _userRepository.IsManagerExistByIdAsync(managerId!.Value)
            )
            {
                throw new Exception(
                    $"Manager with ID '{managerId}' does not exist."
                );
            }

            if (
                !await _branchRepository.IsManagerAvailableAsync(
                    managerId.Value
                )
            )
            {
                throw new Exception(
                    $"Manager with ID '{managerId}' is already assigned to another branch."
                ); // Use specific exception and avoid magic exception message.
            }
        }
        #endregion
    }
}
