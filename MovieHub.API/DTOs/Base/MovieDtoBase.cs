using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.DTOs.Base
{
    public abstract class MovieDtoBase
    {
        private string _title = String.Empty;

        [Required(ErrorMessage = ErrorMessages.Movie.TitleRequired)]
        [MaxLength(
            ValidationConstants.Movie.MaxTitleLength,
            ErrorMessage = ErrorMessages.Movie.TitleMaxLengthExceeded
        )]
        [RegularExpression(
            ValidationConstants.Movie.MovieTitleRegex,
            ErrorMessage = ErrorMessages.Movie.InvalidTitleFormat
        )]
        public string Title
        {
            get => _title;
            set => _title = value?.Trim() ?? String.Empty;
        }

        private string _genre = String.Empty;

        [Required(ErrorMessage = ErrorMessages.Movie.GenreRequired)]
        [MaxLength(
            ValidationConstants.Movie.MaxGenreLength,
            ErrorMessage = ErrorMessages.Movie.GenreMaxLengthExceeded
        )]
        [RegularExpression(
            ValidationConstants.Movie.GenreRegex,
            ErrorMessage = ErrorMessages.Movie.InvalidGenreFormat
        )]
        public string Genre
        {
            get => _genre;
            set => _genre = value?.Trim() ?? String.Empty;
        }

        [Required(ErrorMessage = ErrorMessages.Movie.DurationRequired)]
        [Range(
            ValidationConstants.Movie.MinDuration,
            ValidationConstants.Movie.MaxDuration,
            ErrorMessage = ErrorMessages.Movie.DurationOutOfRange
        )] // Duration in minutes
        public int Duration { get; set; }

        private string _director = String.Empty;

        [Required(ErrorMessage = ErrorMessages.PersonNameRequired)]
        [MaxLength(
            ValidationConstants.MaxPersonNameLength,
            ErrorMessage = ErrorMessages.PersonNameMaxLenghtExceeded
        )]
        [RegularExpression(
            ValidationConstants.PersonNameRegex,
            ErrorMessage = ErrorMessages.InvalidPersonNameFormat
        )]
        public string Director
        {
            get => _director;
            set => _director = value?.Trim() ?? String.Empty;
        }

        [MinLength(ValidationConstants.Movie.MinDescriptionLength)]
        [MaxLength(ValidationConstants.Movie.MaxDescriptionLength)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessages.Movie.PosterUrlRequired)]
        [MaxLength(
            ValidationConstants.Movie.MaxPosterUrlLength,
            ErrorMessage = ErrorMessages.Movie.PosterUrlMaxLengthExceeded
        )]
        [Url(ErrorMessage = ErrorMessages.Movie.InvalidPosterUrlFormat)]
        public string PosterUrl { get; set; } = string.Empty;

        private string _actors = String.Empty;

        [Required(ErrorMessage = ErrorMessages.Movie.InvalidActorsFormat)]
        [MaxLength(ValidationConstants.Movie.MaxActorsLength)]
        [RegularExpression(
            ValidationConstants.NameRegex,
            ErrorMessage = ErrorMessages.InvalidNameFormat
        )]
        public string Actors
        {
            get => _actors;
            set => _actors = value?.Trim() ?? String.Empty;
        }
    }
}
