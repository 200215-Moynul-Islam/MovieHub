using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieHub.API.Constants;
using MovieHub.API.Models.Base;

namespace MovieHub.API.Models
{
    public class ShowTime : EntityBase
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        [Range(
            ValidationConstants.ShowTime.MinBufferMinutes,
            ValidationConstants.ShowTime.MaxBufferMinutes
        )]
        public int BufferMinutes { get; set; }

        [Required]
        public int MovieId { get; set; } // Foreign Key to Movie

        [Required]
        public int HallId { get; set; } // Foreign Key to Hall

        // Navigation Properties
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; } = null!; // Navigation property to related Movie

        [ForeignKey("HallId")]
        public Hall Hall { get; set; } = null!; // Navigation property to related Hall

        public ICollection<Booking> Bookings { get; set; } =
            new List<Booking>(); // Navigation property to related Bookings
    }
}
