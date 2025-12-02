using MovieHub.API.Constants;
using MovieHub.API.Exceptions;
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
                throw new NotFoundException(
                    BusinessErrorMessages.Hall.NotFound
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
                throw new ConflictException(
                    BusinessErrorMessages.Hall.HasUpcomingShowTimes
                );
            }
        }
        #endregion
    }
}
