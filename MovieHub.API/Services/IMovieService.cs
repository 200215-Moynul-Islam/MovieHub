using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IMovieService
    {
        Task<int> CreateMovieAsync(MovieCreateDto movieCreateDto);
    }
}
