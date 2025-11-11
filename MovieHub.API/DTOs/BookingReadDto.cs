using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class BookingReadDto : BookingDtoBase
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(
            ValidationConstants.Booking.MinSeats,
            ErrorMessage = ErrorMessages.Booking.MinSeatsRequired
        )]
        public ICollection<BookingSeatReadDto>? BookedSeats { get; set; }
    }
}
