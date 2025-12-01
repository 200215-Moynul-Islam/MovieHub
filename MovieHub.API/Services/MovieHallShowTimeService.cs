using AutoMapper;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Exceptions;
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

        public async Task<ShowTimeReadDto> CreateShowTimeAsync(
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
            return _mapper.Map<ShowTimeReadDto>(showTime);
        }

        public async Task<ShowTimeReadDto> UpdateShowTimeByIdAsync(
            int showTimeId,
            ShowTimeUpdateDto showTimeUpdateDto
        )
        {
            var showTime = await GetShowTimeByIdOrThrowAsync(showTimeId);
            EnsureShowTimeHasNotStartedOrThrow(showTime);

            // Get the movie duration using the updated MovieId, or use the existing one if MovieId is not provided in the ShowTimeUpdateDto.
            var movieDuration = await GetMovieDurationByMovieIdOrThrowAsync(
                showTimeUpdateDto.MovieId is null
                    ? showTime.MovieId!.Value
                    : showTimeUpdateDto.MovieId.Value
            );

            // Use the existing HallId . If an updated HallId is provided, verify it exists before using it.
            var hallId = showTime.HallId!.Value;
            if (showTimeUpdateDto.HallId is not null)
            {
                await EnsureHallExistByIdOrThrowAsync(
                    showTimeUpdateDto.HallId.Value
                );
                hallId = showTimeUpdateDto.HallId.Value;
            }

            // Use the updated StartTime if provided; otherwise, keep the existing one.
            var startTime = showTimeUpdateDto.StartTime is not null
                ? showTimeUpdateDto.StartTime.Value
                : showTime.StartTime!.Value;
            // Use the updated BufferMinutes if provided; otherwise, keep the existing one.
            var bufferMinutes = showTimeUpdateDto.BufferMinutes is not null
                ? showTimeUpdateDto.BufferMinutes.Value
                : showTime.BufferMinutes!.Value;
            await EnsureNoConflictingShowTimeExistsInHallOrThrowAsync(
                startTime,
                startTime.AddMinutes(movieDuration + bufferMinutes),
                hallId,
                showTimeId
            );

            _mapper.Map(showTimeUpdateDto, showTime);
            await _showTimeRepository.SaveChangesAsync();
            return _mapper.Map<ShowTimeReadDto>(showTime);
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
                throw new NotFoundException(
                    BusinessErrorMessages.Movie.NotFound
                );
            }
            return movieDuration.Value;
        }

        private async Task EnsureHallExistByIdOrThrowAsync(int id)
        {
            if (!await _hallRepository.ExistsByIdAsync(id))
            {
                throw new NotFoundException(
                    BusinessErrorMessages.Hall.NotFound
                );
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
                throw new ConflictException(
                    BusinessErrorMessages.ShowTime.Conflict
                );
            }
        }

        private async Task EnsureNoConflictingShowTimeExistsInHallOrThrowAsync(
            DateTime startTime,
            DateTime endTime,
            int hallId,
            int currentShowTimeId
        )
        {
            if (
                await _showTimeRepository.HasAnyConflictingShowTimeInHallAsync(
                    startTime,
                    endTime,
                    hallId,
                    currentShowTimeId
                )
            )
            {
                throw new ConflictException(
                    BusinessErrorMessages.ShowTime.Conflict
                );
            }
        }

        private async Task<ShowTime> GetShowTimeByIdOrThrowAsync(int showTimeId)
        {
            var showTime = await _showTimeRepository.GetByIdAsync(showTimeId);
            if (showTime is null)
            {
                throw new NotFoundException(
                    BusinessErrorMessages.ShowTime.NotFound
                );
            }
            return showTime;
        }

        private void EnsureShowTimeHasNotStartedOrThrow(ShowTime showTime)
        {
            if (showTime.StartTime <= DateTime.UtcNow)
            {
                throw new ConflictException(
                    BusinessErrorMessages.ShowTime.Started
                );
            }
        }
        #endregion
    }
}
