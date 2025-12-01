using AutoMapper;
using MovieHub.API.Constants;
using MovieHub.API.DTOs;
using MovieHub.API.Exceptions;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class UserShowTimeBookingService : IUserShowTimeBookingService
    {
        private readonly IMapper _mapper;
        private readonly IShowTimeRepository _showTimeRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;

        public UserShowTimeBookingService(
            IMapper mapper,
            IShowTimeRepository showTimeRepository,
            IBookingRepository bookingRepository,
            IUserRepository userRepository
        )
        {
            _mapper = mapper;
            _showTimeRepository = showTimeRepository;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
        }

        public async Task<BookingReadDto> CreateBookingAsync(
            BookingCreateDto bookingCreateDto
        )
        {
            await EnsureUserExistsByIdOrThrowAsync(
                bookingCreateDto.UserId!.Value
            );
            var showTime = await GetShowTimeWithHallAndBookedSeatsOrThrowAsync(
                bookingCreateDto.ShowTimeId!.Value
            );
            EnsureShowIsNotStarted(showTime);
            EnsureSeatsExistForShowTimeOrThrow(
                bookingCreateDto.SeatIds!,
                showTime
            );
            EnsureSeatsAreAvailableOrThrow(bookingCreateDto.SeatIds!, showTime);

            var booking = _mapper.Map<Booking>(bookingCreateDto);
            await _bookingRepository.CreateAsync(booking);
            await _bookingRepository.SaveChangesAsync();
            return _mapper.Map<BookingReadDto>(booking);
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

        private async Task<ShowTime> GetShowTimeWithHallAndBookedSeatsOrThrowAsync(
            int showTimeId
        )
        {
            var showTime =
                await _showTimeRepository.GetShowTimeWithHallAndBookedSeatsAsync(
                    showTimeId
                );
            if (showTime is null)
            {
                throw new NotFoundException(
                    BusinessErrorMessages.ShowTime.NotFound
                );
            }
            return showTime;
        }

        private void EnsureShowIsNotStarted(ShowTime showTime)
        {
            if (showTime.StartTime <= DateTime.UtcNow)
            {
                throw new ConflictException(
                    BusinessErrorMessages.ShowTime.Started
                );
            }
        }

        private void EnsureSeatsExistForShowTimeOrThrow(
            IEnumerable<int> seatIds,
            ShowTime showTime
        )
        {
            var seatIdsForShowTime = showTime
                .Hall.Seats.Select(s => s.Id)
                .ToHashSet();
            var invalidDesiredSeatIds = seatIds
                .Where(sid => !seatIdsForShowTime.Contains(sid))
                .ToList();
            if (invalidDesiredSeatIds.Any())
            {
                throw new NotFoundException(
                    BusinessErrorMessages.Booking.NotFoundSeatsFailure
                );
            }
        }

        private void EnsureSeatsAreAvailableOrThrow(
            IEnumerable<int> seatIds,
            ShowTime showTime
        )
        {
            var bookedSeatIds = showTime
                .Bookings.SelectMany(b => b.BookedSeats)
                .Select(bs => bs.SeatId)
                .ToHashSet();
            var unavailableDesiredSeatIds = seatIds
                .Where(sid => bookedSeatIds.Contains(sid))
                .ToList();
            if (unavailableDesiredSeatIds.Any())
            {
                throw new ConflictException(
                    BusinessErrorMessages.Booking.UnavailableSeatsFailure
                );
            }
        }
        #endregion
    }
}
