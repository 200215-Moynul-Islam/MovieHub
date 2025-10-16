using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.DTOs.Base
{
    public abstract class NullableNamedDtoBase
    {
        private string? _name;

        [MaxLength(
            ValidationConstants.MaxNameLength,
            ErrorMessage = ErrorMessages.NameMaxLengthExceeded
        )]
        [RegularExpression(
            ValidationConstants.NameRegex,
            ErrorMessage = ErrorMessages.InvalidNameFormat
        )]
        public string? Name
        {
            get => _name;
            set => _name = value?.Trim();
        }
    }
}
