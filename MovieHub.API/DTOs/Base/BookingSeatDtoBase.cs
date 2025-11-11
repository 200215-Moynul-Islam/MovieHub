using System.ComponentModel.DataAnnotations;

namespace MovieHub.API.DTOs.Base
{
    public abstract class BookingSeatDtoBase
    {
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int SeatId { get; set; }
    }
}
