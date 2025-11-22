using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IMovieHallShowTimeService
    {
        Task<ShowTimeReadDto> CreateShowTimeAsync(
            ShowTimeCreateDto showTimeCreateDto
        );
        Task<ShowTimeReadDto> UpdateShowTimeByIdAsync(
            int id,
            ShowTimeUpdateDto showTimeUpdateDto
        );
    }
}
