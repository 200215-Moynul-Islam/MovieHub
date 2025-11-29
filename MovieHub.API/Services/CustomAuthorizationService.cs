using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class CustomAuthorizationService : ICustomAuthorizationService
    {
        private readonly IHallRepository _hallRepository;
        private readonly IShowTimeRepository _showTimeRepository;

        public CustomAuthorizationService(
            IHallRepository hallRepository,
            IShowTimeRepository showTimeRepository
        )
        {
            _hallRepository = hallRepository;
            _showTimeRepository = showTimeRepository;
        }

        public async Task<bool> IsManagerByHallIdAsync(int hallId, Guid userId)
        {
            var managerId = await GetManagerIdByHallIdOrThrowAsync(hallId);
            return userId == managerId;
        }

        public async Task<bool> IsManagerByShowTimeIdAsync(
            int showTimeId,
            Guid userId
        )
        {
            var managerId = await GetManagerIdByShowTimeIdOrThrowAsync(
                showTimeId
            );
            return userId == managerId;
        }

        #region Private Methods
        private async Task<Guid?> GetManagerIdByHallIdOrThrowAsync(int hallId)
        {
            await EnsureHallExistsByHallIdOrThrowAsync(hallId);
            return await _hallRepository.GetManagerIdByIdAsync(hallId);
        }

        private async Task EnsureHallExistsByHallIdOrThrowAsync(int hallId)
        {
            if (!await _hallRepository.ExistsByIdAsync(hallId))
            {
                throw new Exception($"Hall with Id {hallId} does not exists.");
            }
        }

        private async Task<Guid?> GetManagerIdByShowTimeIdOrThrowAsync(
            int showTimeId
        )
        {
            await EnsureShowTimeExistsByIdOrThrowAsync(showTimeId);
            return await _showTimeRepository.GetManagerIdByIdAsync(showTimeId);
        }

        private async Task EnsureShowTimeExistsByIdOrThrowAsync(int showTimeId)
        {
            if (!await _showTimeRepository.ExistsByIdAsync(showTimeId))
            {
                throw new Exception(
                    $"ShowTime with Id {showTimeId} does not exist."
                );
            }
        }
        #endregion
    }
}
