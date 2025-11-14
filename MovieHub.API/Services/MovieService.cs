using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMapper mapper, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        public async Task<int> CreateMovieAsync(MovieCreateDto movieCreateDto)
        {
            await EnsureMovieTitleIsUniqueOrThrowAsync(movieCreateDto.Title!);
            var movie = _mapper.Map<Movie>(movieCreateDto);
            await _movieRepository.CreateAsync(movie);
            await _movieRepository.SaveChangesAsync();
            return movie.Id;
        }

        public async Task<MovieReadDto> GetMovieByIdAsync(int id)
        {
            var movie = await GetMovieByIdOrThrowAsync(id);
            return _mapper.Map<MovieReadDto>(movie);
        }

        public async Task<IEnumerable<MovieReadDto>> GetAllMoviesAsync(
            int offset,
            int limit
        )
        {
            return _mapper.Map<IEnumerable<MovieReadDto>>(
                await _movieRepository.GetAllAsync(offset, limit)
            );
        }

        public async Task UpdateMovieByIdAsync(
            int id,
            MovieUpdateDto movieUpdateDto
        )
        {
            if (movieUpdateDto.Title is not null)
            {
                await EnsureMovieTitleIsUniqueOrThrowAsync(
                    movieUpdateDto.Title
                );
            }

            var movie = await GetMovieByIdOrThrowAsync(id);
            _mapper.Map(movieUpdateDto, movie);
            await _movieRepository.SaveChangesAsync();
            return;
        }

        #region Private Methods
        private async Task EnsureMovieTitleIsUniqueOrThrowAsync(string title)
        {
            if (await _movieRepository.TitleExistsCaseInsensitiveAsync(title))
            {
                throw new Exception(
                    $"Movie with title '{title}' already exists."
                );
            }
        }

        private async Task<Movie> GetMovieByIdOrThrowAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie is null)
            {
                throw new Exception($"Movie with id '{id}' does not exists.");
            }
            return movie;
        }
        #endregion
    }
}
