using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? null : value;
                _name = value is not null
                    ? Regex.Replace(value.Trim(), @"\s+", " ")
                    : value;
            }
        }
    }
}
