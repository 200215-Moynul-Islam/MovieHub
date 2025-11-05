using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class BranchHallService : IBranchHallService
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        private readonly IHallRepository _hallRepository;

        public BranchHallService(
            IMapper mapper,
            IBranchRepository branchRepository,
            IHallRepository hallRepository
        )
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
            _hallRepository = hallRepository;
        }

        public async Task DeactivateBranchWithHallsByBranchIdAsync(int branchId)
        {
            await EnsureBranchExistsByIdOrThrowAsync(branchId);

            await _branchRepository.ExecuteInTransactionAsync(async () =>
            {
                await _branchRepository.DeactivateByIdAsync(branchId);
                await _hallRepository.DeactivateHallsByBranchIdAsync(branchId);
            });
        }

        public async Task<IEnumerable<HallReadDto>> GetHallsByBranchIdAsync(
            int branchId,
            int offset,
            int limit
        )
        {
            await EnsureBranchExistsByIdOrThrowAsync(branchId);
            return _mapper.Map<IEnumerable<HallReadDto>>(
                await _hallRepository.GetHallsByBranchIdAsync(
                    branchId,
                    offset,
                    limit
                )
            );
        }

        #region Private Methods
        private async Task EnsureBranchExistsByIdOrThrowAsync(int id)
        {
            bool exists = await _branchRepository.ExistsByIdAsync(id);
            if (!exists)
            {
                throw new Exception($"Branch with id '{id}' does not exist.");
            }
        }
        #endregion
    }
}
