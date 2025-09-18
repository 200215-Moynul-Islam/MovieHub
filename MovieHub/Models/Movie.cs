using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieHub.API.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [Range(1, 500)] // Duration in minutes
        public int Duration { get; set; }

        [Required]
        [MaxLength(100)]
        public string Director { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string PosterUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Actors { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>(); // Navigation property to related Showtimes
    }
}
