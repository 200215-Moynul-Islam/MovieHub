using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IBranchHallSeatService
    {
        Task<int> CreateHallWithSeatsAsync(HallCreateDto hallCreateDto);
    }
}
