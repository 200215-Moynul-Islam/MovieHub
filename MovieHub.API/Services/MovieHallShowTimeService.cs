using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class MovieHallShowTimeService : IMovieHallShowTimeService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IShowTimeRepository _showTimeRepository;

        public MovieHallShowTimeService(
            IMapper mapper,
            IMovieRepository movieRepository,
            IHallRepository hallRepository,
            IShowTimeRepository showTimeRepository
        )
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _hallRepository = hallRepository;
            _showTimeRepository = showTimeRepository;
        }

        public async Task<int> CreateShowTimeAsync(
            ShowTimeCreateDto showTimeCreateDto
        )
        {
            var movieDuration = await GetMovieDurationByMovieIdOrThrowAsync(
                showTimeCreateDto.MovieId!.Value
            );
            var hallId = showTimeCreateDto.HallId!.Value;
            await EnsureHallExistByIdOrThrowAsync(hallId);
            var startTime = showTimeCreateDto.StartTime!.Value;
            await EnsureNoConflictingShowTimeExistsInHallOrThrowAsync(
                startTime,
                startTime.AddMinutes(
                    movieDuration + showTimeCreateDto.BufferMinutes!.Value
                ),
                hallId
            );

            var showTime = _mapper.Map<ShowTime>(showTimeCreateDto);
            await _showTimeRepository.CreateAsync(showTime);
            await _showTimeRepository.SaveChangesAsync();
            return showTime.Id;
        }

        #region Private Methods
        private async Task<int> GetMovieDurationByMovieIdOrThrowAsync(
            int movieId
        )
        {
            var movieDuration =
                await _movieRepository.GetMovieDurationByMovieIdAsync(movieId);
            if (movieDuration is null)
            {
                throw new Exception(
                    $"Movie with id '{movieId}' does not exist."
                );
            }
            return movieDuration.Value;
        }

        private async Task EnsureHallExistByIdOrThrowAsync(int id)
        {
            if (!await _hallRepository.ExistsByIdAsync(id))
            {
                throw new Exception($"Hall with id '{id}' does not exist.");
            }
        }

        private async Task EnsureNoConflictingShowTimeExistsInHallOrThrowAsync(
            DateTime startTime,
            DateTime endTime,
            int hallId
        )
        {
            if (
                await _showTimeRepository.HasAnyConflictingShowTimeInHallAsync(
                    startTime,
                    endTime,
                    hallId
                )
            )
            {
                throw new Exception(
                    "Cannot schedule this show. It overlaps with another show in the same hall."
                );
            }
        }
        #endregion
    }
}
