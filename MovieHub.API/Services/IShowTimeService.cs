using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IShowTimeService
    {
        Task<ShowTimeDetailsReadDto> GetShowTimeDetailsByIdAsync(int id);
    }
}
