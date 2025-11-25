using MovieHub.API.DTOs;

namespace MovieHub.API.Services
{
    public interface IUserShowTimeBookingService
    {
        Task<BookingReadDto> CreateBookingAsync(
            BookingCreateDto bookingCreateDto
        );
    }
}
