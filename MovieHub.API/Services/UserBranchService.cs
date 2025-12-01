using AutoMapper;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Exceptions;
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
                throw new NotFoundException(
                    BusinessErrorMessages.Branch.NotFound
                );
            }
            return branch;
        }

        private async Task EnsureBranchNameIsUniqueOrThrowAsync(string name)
        {
            if (await _branchRepository.NameExistsCaseInsensitiveAsync(name))
            {
                throw new ConflictException(
                    BusinessErrorMessages.Branch.NameUnavailable
                );
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
                throw new NotFoundException(
                    BusinessErrorMessages.User.ManagerNotFound
                );
            }

            if (
                !await _branchRepository.IsManagerAvailableAsync(
                    managerId.Value
                )
            )
            {
                throw new ConflictException(
                    BusinessErrorMessages.User.ManagerUnavailable
                );
            }
        }
        #endregion
    }
}
