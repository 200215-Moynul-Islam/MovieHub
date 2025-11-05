using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IHallService
    {
        Task<HallReadDto> GetHallByIdAsync(int id);
        Task UpdateHallByIdAsync(int id, HallUpdateDto hallUpdateDto);
    }
}
