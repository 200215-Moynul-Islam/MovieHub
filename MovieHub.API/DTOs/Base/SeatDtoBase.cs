using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.DTOs.Base
{
    public abstract class SeatDtoBase
    {
        [Required]
        [StringLength(
            ValidationConstants.Seat.MaxSeatNumberLength,
            MinimumLength = ValidationConstants.Seat.MinSeatNumberLength
        )]
        public string? SeatNumber { get; set; }

        [Required]
        public int? HallId { get; set; }
    }
}
