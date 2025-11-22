using AutoMapper;
using MovieHub.API.DTOs;
using MovieHub.API.Models;
using MovieHub.API.Repositories;

namespace MovieHub.API.Services
{
    public class UserShowTimeBookingService : IUserShowTimeBookingService
    {
        private readonly IMapper _mapper;
        private readonly IShowTimeRepository _showTimeRepository;
        private readonly IBookingRepository _bookingRepository;

        public UserShowTimeBookingService(
            IMapper mapper,
            IShowTimeRepository showTimeRepository,
            IBookingRepository bookingRepository
        )
        {
            _mapper = mapper;
            _showTimeRepository = showTimeRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingReadDto> CreateBookingAsync(
            BookingCreateDto bookingCreateDto
        )
        {
            // TODO: Validate UserId when User table is implemented.
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
                throw new Exception(
                    $"ShowTime with id '{showTimeId}' does not exist."
                );
            }
            return showTime;
        }

        private void EnsureShowIsNotStarted(ShowTime showTime)
        {
            if (showTime.StartTime <= DateTime.UtcNow)
            {
                throw new Exception(
                    "Booking failed. This show is no longer available for booking as it has already started."
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
                throw new Exception(
                    $"Booking failed. Following seat Ids do not exist for the show: {string.Join(", ", invalidDesiredSeatIds)}"
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
                throw new Exception(
                    $"Booking faild. Following seat Ids are not available: {String.Join(", ", unavailableDesiredSeatIds)}"
                );
            }
        }
        #endregion
    }
}
