using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.DTOs.Base
{
    public abstract class UserDtoBase
    {
        private string? _email;

        [Required(ErrorMessage = ErrorMessages.User.EmailRequired)]
        [StringLength(
            ValidationConstants.User.MaxEmailLength,
            ErrorMessage = ErrorMessages.User.EmailMaxLengthExceeded
        )]
        [EmailAddress(ErrorMessage = ErrorMessages.User.InvalidEmailFormat)]
        public string? Email
        {
            get => _email;
            set
            {
                _email = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
            }
        }

        private string? _username;

        [Required(ErrorMessage = ErrorMessages.User.UsernameRequired)]
        [StringLength(
            ValidationConstants.User.MaxUsernameLength,
            MinimumLength = ValidationConstants.User.MinUsernameLength,
            ErrorMessage = ErrorMessages.User.UsernameLengthOutOfRange
        )]
        [RegularExpression(
            ValidationConstants.User.UsernameRegex,
            ErrorMessage = ErrorMessages.User.InvalidUsernameFormat
        )]
        public string? Username
        {
            get => _username;
            set
            {
                _username = string.IsNullOrEmpty(value) ? null : value.Trim();
            }
        }
    }
}
