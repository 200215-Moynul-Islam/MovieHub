using AutoMapper;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Exceptions;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class UserBookingService : IUserBookingService
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;

        public UserBookingService(
            IMapper mapper,
            IBookingRepository bookingRepository,
            IUserRepository userRepository
        )
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
        }

        public async Task<
            IEnumerable<BookingReadDto>
        > GetAllBookingsWithSeatsByUserIdAsync(
            Guid userId,
            int offset,
            int limit
        )
        {
            await EnsureUserExistsByIdOrThrowAsync(userId);
            return _mapper.Map<IEnumerable<BookingReadDto>>(
                await _bookingRepository.GetAllBookingsWithSeatsByUserIdAsync(
                    userId,
                    offset,
                    limit
                )
            );
        }

        #region Private Methods
        private async Task EnsureUserExistsByIdOrThrowAsync(Guid id)
        {
            if (!await _userRepository.ExistsByIdAsync(id))
            {
                throw new NotFoundException(
                    BusinessErrorMessages.User.NotFound
                );
            }
        }
        #endregion
    }
}
