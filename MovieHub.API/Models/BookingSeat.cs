using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieHub.API.Models
{
    public class BookingSeat
    {
        // Composite Key: BookingId + SeatId. Configure it in DbContext using Fluent API
        // because Data annotation has not this facility.)
        [Required]
        public int BookingId { get; set; } // Foreign Key to Booking

        [Required]
        public int SeatId { get; set; } // Foreign Key to Seat

        // Navigation Properties
        [ForeignKey("BookingId")]
        public Booking Booking { get; set; } = null!; // Navigation property to related Booking

        [ForeignKey("SeatId")]
        public Seat Seat { get; set; } = null!; // Navigation property to related Seat
    }
}
