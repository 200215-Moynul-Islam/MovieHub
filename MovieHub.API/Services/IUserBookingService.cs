using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IUserBookingService
    {
        Task<IEnumerable<BookingReadDto>> GetAllBookingsByUserIdAsync(
            Guid userId,
            int offset,
            int limit
        );
    }
}
