using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MovieHub.API.Constants;

namespace MovieHub.API.DTOs.Base
{
    public abstract class MovieDtoBase
    {
        private string? _title;

        [Required(ErrorMessage = ErrorMessages.Movie.TitleRequired)]
        [MaxLength(
            ValidationConstants.Movie.MaxTitleLength,
            ErrorMessage = ErrorMessages.Movie.TitleMaxLengthExceeded
        )]
        [RegularExpression(
            ValidationConstants.Movie.MovieTitleRegex,
            ErrorMessage = ErrorMessages.Movie.InvalidTitleFormat
        )]
        public string? Title
        {
            get => _title;
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? null : value;
                _title = value is not null
                    ? Regex.Replace(value.Trim(), @"\s+", " ")
                    : value;
            }
        }

        private string? _genre;

        [Required(ErrorMessage = ErrorMessages.Movie.GenreRequired)]
        [MaxLength(
            ValidationConstants.Movie.MaxGenreLength,
            ErrorMessage = ErrorMessages.Movie.GenreMaxLengthExceeded
        )]
        [RegularExpression(
            ValidationConstants.Movie.GenreRegex,
            ErrorMessage = ErrorMessages.Movie.InvalidGenreFormat
        )]
        public string? Genre
        {
            get => _genre;
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? null : value;
                _genre = value is not null
                    ? Regex.Replace(value.Trim(), @"\s+", " ")
                    : value;
            }
        }

        [Required(ErrorMessage = ErrorMessages.Movie.DurationRequired)]
        [Range(
            ValidationConstants.Movie.MinDuration,
            ValidationConstants.Movie.MaxDuration,
            ErrorMessage = ErrorMessages.Movie.DurationOutOfRange
        )] // Duration in minutes
        public int? Duration { get; set; }

        private string? _director;

        [Required(ErrorMessage = ErrorMessages.PersonNameRequired)]
        [MaxLength(
            ValidationConstants.MaxPersonNameLength,
            ErrorMessage = ErrorMessages.PersonNameMaxLenghtExceeded
        )]
        [RegularExpression(
            ValidationConstants.PersonNameRegex,
            ErrorMessage = ErrorMessages.InvalidPersonNameFormat
        )]
        public string? Director
        {
            get => _director;
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? null : value;
                _director = value is not null
                    ? Regex.Replace(value.Trim(), @"\s+", " ")
                    : value;
            }
        }

        private string? _description;

        [Required]
        [MinLength(
            ValidationConstants.Movie.MinDescriptionLength,
            ErrorMessage = ErrorMessages.Movie.DescriptionMinLengthRequired
        )]
        [MaxLength(
            ValidationConstants.Movie.MaxDescriptionLength,
            ErrorMessage = ErrorMessages.Movie.DescriptionMaxLengthExceeded
        )]
        public string? Description
        {
            get => _description;
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? null : value;
                _description = value is not null
                    ? Regex.Replace(value.Trim(), @"\s+", " ")
                    : value;
            }
        }

        private string? _posterUrl;

        [Required(ErrorMessage = ErrorMessages.Movie.PosterUrlRequired)]
        [MaxLength(
            ValidationConstants.Movie.MaxPosterUrlLength,
            ErrorMessage = ErrorMessages.Movie.PosterUrlMaxLengthExceeded
        )]
        [Url(ErrorMessage = ErrorMessages.Movie.InvalidPosterUrlFormat)]
        public string? PosterUrl
        {
            get => _posterUrl;
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? null : value;
                _posterUrl = value is not null ? value.Trim() : value;
            }
        }

        private string? _actors;

        [Required(ErrorMessage = ErrorMessages.Movie.ActorsRequired)]
        [MaxLength(
            ValidationConstants.Movie.MaxActorsLength,
            ErrorMessage = ErrorMessages.Movie.ActorsMaxLengthExceeded
        )]
        [RegularExpression(
            ValidationConstants.Movie.ActorsRegex,
            ErrorMessage = ErrorMessages.Movie.InvalidActorsFormat
        )]
        public string? Actors
        {
            get => _actors;
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? null : value;
                _actors = value is not null
                    ? Regex.Replace(value.Trim(), @"\s+", " ")
                    : value;
            }
        }
    }
}
