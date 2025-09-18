using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieHub.API.Models
{
    public class ShowTime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public int MovieId { get; set; } // Foreign Key to Movie

        [Required]
        public int HallId { get; set; } // Foreign Key to Hall

        // Navigation Properties
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; } = null!; // Navigation property to related Movie

        [ForeignKey("HallId")]
        public Hall Hall { get; set; } = null!; // Navigation property to related Hall

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>(); // Navigation property to related Bookings
    }
}
