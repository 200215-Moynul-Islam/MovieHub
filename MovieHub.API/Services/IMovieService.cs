using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IMovieService
    {
        Task<int> CreateMovieAsync(MovieCreateDto movieCreateDto);
        Task<MovieReadDto> GetMovieByIdAsync(int Id);
        Task<IEnumerable<MovieReadDto>> GetAllMoviesAsync(
            int offset,
            int limit
        );
        Task UpdateMovieByIdAsync(int id, MovieUpdateDto movieUpdateDto);
    }
}
