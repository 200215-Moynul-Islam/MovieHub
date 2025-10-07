using MovieHub.API.Constants;
using MovieHub.API.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace MovieHub.API.Models
{
    public class Movie : SoftDeletableEntityBase
    {
        [Required]
        [MaxLength(ValidationConstants.MaxMovieTitleLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.MaxMovieGenreLength)]
        public string Genre { get; set; } = string.Empty;

        [Required]
        [Range(ValidationConstants.MinMovieDuration, ValidationConstants.MaxMovieDuration)] // Duration in minutes
        public int Duration { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxMovieDirectorLength)]
        public string Director { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.MaxMovieDescriptionLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.MaxMoviePosterUrlLength)]
        public string PosterUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(ValidationConstants.MaxMovieActorsLength)]
        public string Actors { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>(); // Navigation property to related Showtimes
    }
}
