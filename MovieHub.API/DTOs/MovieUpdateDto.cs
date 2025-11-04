using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MovieHub.API.Constants;

namespace MovieHub.API.DTOs
{
    public class MovieUpdateDto
    {
        private string? _title;

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

        [Range(
            ValidationConstants.Movie.MinDuration,
            ValidationConstants.Movie.MaxDuration,
            ErrorMessage = ErrorMessages.Movie.DurationOutOfRange
        )] // Duration in minutes
        public int? Duration { get; set; }

        private string? _director;

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

        [MaxLength(
            ValidationConstants.Movie.MaxPosterUrlLength,
            ErrorMessage = ErrorMessages.Movie.PosterUrlMaxLengthExceeded
        )]
        [Url(ErrorMessage = ErrorMessages.Movie.InvalidPosterUrlFormat)]
        public string? PosterUrl
        {
            get => _posterUrl;
            set => _posterUrl = value?.Trim();
        }

        private string? _actors;

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
