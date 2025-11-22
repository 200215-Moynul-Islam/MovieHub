using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IBranchHallSeatService
    {
        Task<HallReadDto> CreateHallWithSeatsAsync(HallCreateDto hallCreateDto);
    }
}
