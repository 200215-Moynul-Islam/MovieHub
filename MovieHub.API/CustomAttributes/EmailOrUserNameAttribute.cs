using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MovieHub.API.Constants;

namespace MovieHub.API.CustomAttributes
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false
    )]
    public class EmailOrUsernameAttribute : ValidationAttribute
    {
        private readonly EmailAddressAttribute _emailValidator =
            new EmailAddressAttribute();

        public EmailOrUsernameAttribute()
        {
            // Default error message
            ErrorMessage = ErrorMessages
                .CustomAttributes
                .EmailOrUsername_Default;
        }

        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            // If null skip validation; [Required] handles null check
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var stringValue = value.ToString()!;
            if (IsValidEmail(stringValue) || IsValidUsername(stringValue))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(
                FormatErrorMessage(validationContext.DisplayName)
            );
        }

        #region Private methods
        private bool IsValidEmail(string email)
        {
            return _emailValidator.IsValid(email);
        }

        private bool IsValidUsername(string username)
        {
            return Regex.IsMatch(
                username,
                ValidationConstants.User.UsernameRegex
            );
        }
        #endregion
    }
}
