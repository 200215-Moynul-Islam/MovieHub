using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class BookingCreateDto : BookingDtoBase
    {
        [Required]
        [MinLength(
            ValidationConstants.Booking.MinSeats,
            ErrorMessage = ErrorMessages.Booking.MinSeatsRequired
        )]
        public IEnumerable<int>? SeatIds { get; set; }
    }
}
