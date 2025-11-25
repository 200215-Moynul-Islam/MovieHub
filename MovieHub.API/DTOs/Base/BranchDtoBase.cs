using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MovieHub.API.Constants;

namespace MovieHub.API.DTOs.Base
{
    public abstract class BranchDtoBase : NamedDtoBase
    {
        private string? _location;

        [Required(ErrorMessage = ErrorMessages.Branch.LocationRequired)]
        [MaxLength(
            ValidationConstants.Branch.MaxLocationLength,
            ErrorMessage = ErrorMessages.Branch.LocationMaxLengthExceeded
        )]
        [RegularExpression(
            ValidationConstants.Branch.LocationRegex,
            ErrorMessage = ErrorMessages.Branch.InvalidLocationFormat
        )]
        public string? Location
        {
            get => _location;
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? null : value;
                _location = value is not null
                    ? Regex.Replace(value.Trim(), @"\s+", " ")
                    : value;
            }
        }
    }
}
