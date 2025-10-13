using MovieHub.API.Constants;
using System.ComponentModel.DataAnnotations;

namespace MovieHub.API.DTOs.Base
{
    public abstract class NullableNamedDtoBase
    {
        private string? _name;
        [MaxLength(ValidationConstants.MaxNameLength,
            ErrorMessage = ErrorMessages.NameMaxLengthExceeded)]
        [RegularExpression(ValidationConstants.NameRegex, 
            ErrorMessage = ErrorMessages.InvalidNameFormat)]
        public string? Name
        {
            get => _name;
            set => _name = value?.Trim();
        }
    }
}
