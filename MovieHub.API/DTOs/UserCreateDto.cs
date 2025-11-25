using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class UserCreateDto : UserDtoBase
    {
        [Required(ErrorMessage = ErrorMessages.User.PasswordRequired)]
        [StringLength(
            ValidationConstants.User.PasswordMaxLength,
            MinimumLength = ValidationConstants.User.PasswordMinLength,
            ErrorMessage = ErrorMessages.User.PasswordLengthOutOfRange
        )]
        [RegularExpression(
            ValidationConstants.User.PasswordRegex,
            ErrorMessage = ErrorMessages.User.InvalidPasswordFormat
        )]
        public string? Password { get; set; }
    }
}
