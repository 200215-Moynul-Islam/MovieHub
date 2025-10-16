using MovieHub.API.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieHub.API.Models
{
    public class Booking : EntityBase
    {
        [Required]
        public DateTime BookingTime { get; set; } = DateTime.UtcNow;

        public Guid? UserId { get; set; } // Foreign Key to User

        [Required]
        public int ShowTimeId { get; set; } // Foreign Key to ShowTime

        // Navigation Properties
        // [ForeignKey("UserId")]
        // public User? User { get; set; } // Navigation property to related User

        [ForeignKey("ShowTimeId")]
        public ShowTime ShowTime { get; set; } = null!; // Navigation property to related ShowTime

        public ICollection<BookingSeat> BookedSeats { get; set; } = new List<BookingSeat>(); // Navigation property to related BookingSeats
    }
}
