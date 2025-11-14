using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class UserBookingService : IUserBookingService
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;

        public UserBookingService(
            IMapper mapper,
            IBookingRepository bookingRepository
        )
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
        }

        public async Task<
            IEnumerable<BookingReadDto>
        > GetAllBookingsWithSeatsByUserIdAsync(
            Guid userId,
            int offset,
            int limit
        )
        {
            // TODO: Validate whether user exists when user table is ready.
            return _mapper.Map<IEnumerable<BookingReadDto>>(
                await _bookingRepository.GetAllBookingsWithSeatsByUserIdAsync(
                    userId,
                    offset,
                    limit
                )
            );
        }
    }
}
