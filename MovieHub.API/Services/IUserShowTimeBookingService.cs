using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IUserShowTimeBookingService
    {
        Task<int> CreateBookingAsync(BookingCreateDto bookingCreateDto);
    }
}
