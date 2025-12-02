using AutoMapper;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Exceptions;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;

        public BranchService(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<BranchReadDto?> GetBranchByIdAsync(int id)
        {
            var branch = await GetBranchByIdOrThrowAsync(id);
            return _mapper.Map<BranchReadDto>(branch);
        }

        public async Task<IEnumerable<BranchReadDto>> GetAllBranchesAsync(
            int offset,
            int limit
        )
        {
            return _mapper.Map<IEnumerable<BranchReadDto>>(
                await _branchRepository.GetAllAsync(offset, limit)
            );
        }

        public async Task ResetBranchManagerByIdAsync(int id)
        {
            await EnsureBranchExistsByIdOrThrowAsync(id);
            await _branchRepository.ResetBranchManagerByIdAsync(id);
            return;
        }

        #region Private Methods
        private async Task EnsureBranchExistsByIdOrThrowAsync(int id)
        {
            bool exists = await _branchRepository.ExistsByIdAsync(id);
            if (!exists)
            {
                throw new NotFoundException(
                    BusinessErrorMessages.Branch.NotFound
                );
            }
        }

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
        #endregion
    }
}
