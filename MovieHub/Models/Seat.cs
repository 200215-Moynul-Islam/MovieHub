using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieHub.API.Models
{
    public class Seat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key

        [Required]
        [StringLength(3, MinimumLength = 2)]
        public string SeatNumber { get; set; } = string.Empty;

        [Required]
        public int HallId { get; set; } // Foreign Key to Hall

        // Navigation Properties
        [ForeignKey("HallId")]
        public Hall Hall { get; set; } = null!; // Navigation property to related Hall

        public ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>(); // Navigation property to related BookingSeats
    }
}
