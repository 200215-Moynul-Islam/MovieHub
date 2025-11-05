using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class HallShowTimeService : IHallShowTimeService
    {
        private readonly IHallRepository _hallRepository;
        private readonly IShowTimeRepository _showTimeRepository;

        public HallShowTimeService(
            IHallRepository hallRepository,
            IShowTimeRepository showTimeRepository
        )
        {
            _hallRepository = hallRepository;
            _showTimeRepository = showTimeRepository;
        }

        public async Task DeactivateHallByIdAsync(int hallId)
        {
            await EnsureHallExistsByIdOrThrowAsync(hallId);
            await EnsureNoUpcomingShowTimeExistsByHallIdOrThrowAsync(hallId);

            await _hallRepository.DeactivateByIdAsync(hallId);
            return;
        }

        #region Private Methods
        private async Task EnsureHallExistsByIdOrThrowAsync(int hallId)
        {
            if (!await _hallRepository.ExistsByIdAsync(hallId))
            {
                throw new Exception(
                    $"Hall with id '{hallId}' does not exists."
                );
            }
        }

        private async Task EnsureNoUpcomingShowTimeExistsByHallIdOrThrowAsync(
            int hallId
        )
        {
            if (
                await _showTimeRepository.HasAnyUpcomingShowTimesByHallIdAsync(
                    hallId
                )
            )
            {
                throw new Exception(
                    $"Hall with id '{hallId}' has upcoming shows."
                );
            }
        }
        #endregion
    }
}
