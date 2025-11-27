using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;
using MovieHub.API.CustomAttributes;

namespace MovieHub.API.DTOs
{
    public class LoginRequestDto
    {
        private string? _emailOrUsername;

        [Required]
        [StringLength(
            ValidationConstants.MaxEmailOrUsernameLength,
            ErrorMessage = ErrorMessages.EmailOrUsernameMaxLengthExceeded
        )]
        [EmailOrUsername]
        public string? EmailOrUsername
        {
            get => _emailOrUsername;
            set
            {
                _emailOrUsername = string.IsNullOrWhiteSpace(value)
                    ? null
                    : value.Trim();
            }
        }

        [Required]
        [RegularExpression(
            ValidationConstants.User.PasswordRegex,
            ErrorMessage = ErrorMessages.User.InvalidPasswordFormat
        )]
        public string? Password { get; set; }
    }
}
