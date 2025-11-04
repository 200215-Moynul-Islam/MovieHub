using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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
            set => _title = Regex.Replace(value.Trim(), @"\s+", " ");
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
            set => _genre = Regex.Replace(value.Trim(), @"\s+", " ");
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
            set => _director = Regex.Replace(value.Trim(), @"\s+", " ");
        }

        private string _description = String.Empty;
        [MinLength(ValidationConstants.Movie.MinDescriptionLength, ErrorMessage = ErrorMessages.Movie.DescriptionMinLengthRequired)]
        [MaxLength(ValidationConstants.Movie.MaxDescriptionLength, ErrorMessage = ErrorMessages.Movie.DescriptionMaxLengthExceeded)]
        public string Description
        {
            get => _description;
            set => _description = Regex.Replace(value.Trim(), @"\s+", " ");
        }

        private string _posterUrl = String.Empty;
        [Required(ErrorMessage = ErrorMessages.Movie.PosterUrlRequired)]
        [MaxLength(
            ValidationConstants.Movie.MaxPosterUrlLength,
            ErrorMessage = ErrorMessages.Movie.PosterUrlMaxLengthExceeded
        )]
        [Url(ErrorMessage = ErrorMessages.Movie.InvalidPosterUrlFormat)]
        public string PosterUrl
        {
            get => _posterUrl;
            set => _posterUrl = value.Trim();
        }

        private string _actors = String.Empty;

        [Required(ErrorMessage = ErrorMessages.Movie.ActorsRequired)]
        [MaxLength(ValidationConstants.Movie.MaxActorsLength, ErrorMessage = ErrorMessages.Movie.ActorsMaxLengthExceeded)]
        [RegularExpression(
            ValidationConstants.Movie.ActorsRegex,
            ErrorMessage = ErrorMessages.Movie.InvalidActorsFormat
        )]
        public string Actors
        {
            get => _actors;
            set => _actors = Regex.Replace(value.Trim(), @"\s+", " ");
        }
    }
}
