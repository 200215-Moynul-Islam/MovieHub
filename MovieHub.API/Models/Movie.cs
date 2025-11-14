using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;
using MovieHub.API.Models.Base;

namespace MovieHub.API.Models
{
    public class Movie : SoftDeletableEntityBase
    {
        [Required]
        [MaxLength(ValidationConstants.Movie.MaxTitleLength)]
        public string? Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.Movie.MaxGenreLength)]
        public string? Genre { get; set; } = string.Empty;

        [Required]
        [Range(
            ValidationConstants.Movie.MinDuration,
            ValidationConstants.Movie.MaxDuration
        )] // Duration in minutes
        public int? Duration { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxNameLength)]
        public string? Director { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.Movie.MaxDescriptionLength)]
        public string? Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.Movie.MaxPosterUrlLength)]
        public string? PosterUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.Movie.MaxActorsLength)]
        public string? Actors { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<ShowTime> ShowTimes { get; set; } =
            new List<ShowTime>(); // Navigation property to related Showtimes
    }
}
