using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class BranchHallService : IBranchHallService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IHallRepository _hallRepository;

        public BranchHallService(
            IBranchRepository branchRepository,
            IHallRepository hallRepository
        )
        {
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
