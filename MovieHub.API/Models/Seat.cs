using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieHub.API.Constants;
using MovieHub.API.Models.Base;

namespace MovieHub.API.Models
{
    public class Seat : EntityBase
    {
        [Required]
        [StringLength(
            ValidationConstants.Seat.MaxSeatNumberLength,
            MinimumLength = ValidationConstants.Seat.MinSeatNumberLength
        )]
        public string? SeatNumber { get; set; } = string.Empty;

        [Required]
        public int? HallId { get; set; } // Foreign Key to Hall

        // Navigation Properties
        [ForeignKey("HallId")]
        public Hall Hall { get; set; } = null!; // Navigation property to related Hall

        public ICollection<BookingSeat> BookingSeats { get; set; } =
            new List<BookingSeat>(); // Navigation property to related BookingSeats
    }
}
