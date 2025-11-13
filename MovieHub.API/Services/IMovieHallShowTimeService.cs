using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IMovieHallShowTimeService
    {
        Task<int> CreateShowTimeAsync(ShowTimeCreateDto showTimeCreateDto);
        Task UpdateShowTimeByIdAsync(
            int id,
            ShowTimeUpdateDto showTimeUpdateDto
        );
    }
}
