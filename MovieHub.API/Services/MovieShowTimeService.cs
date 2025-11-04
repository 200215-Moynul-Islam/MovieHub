using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class MovieShowTimeService : IMovieShowTimeService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IShowTimeRepository _showTimeRepository;

        public MovieShowTimeService(
            IMovieRepository movieRepository,
            IShowTimeRepository showTimeRepository
        )
        {
            _movieRepository = movieRepository;
            _showTimeRepository = showTimeRepository;
        }

        public async Task DeactivateMovieByIdAsync(int movieId)
        {
            await EnsureMovieExistsByIdOrThrowAsync(movieId);
            await EnsureNoUpcomingShowTimeExistsByMovieIdOrThrowAsync(movieId);
            await _movieRepository.DeactivateByIdAsync(movieId);
            return;
        }

        #region Private Methods
        private async Task EnsureMovieExistsByIdOrThrowAsync(int movieId)
        {
            if (!await _movieRepository.ExistsByIdAsync(movieId))
            {
                throw new Exception(
                    $"Movie with id '{movieId}' does not exists."
                );
            }
        }

        private async Task EnsureNoUpcomingShowTimeExistsByMovieIdOrThrowAsync(
            int movieId
        )
        {
            if (
                await _showTimeRepository.HasAnyUpcomingShowTimesByMovieIdAsync(
                    movieId
                )
            )
            {
                throw new Exception($"This movie has upcoming shows.");
            }
        }
        #endregion
    }
}
