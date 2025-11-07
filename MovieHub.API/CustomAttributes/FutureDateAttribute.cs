using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.CustomAttributes
{
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false
    )]
    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute()
        {
            // Default message with placeholder for property name
            ErrorMessage = ErrorMessages.CustomAttributes.FutureDate_Default;
        }

        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext
        )
        {
            // If null, skip validation; [Required] handles null check
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is DateTime dateTime)
            {
                if (dateTime <= DateTime.UtcNow)
                {
                    // Uses FormatErrorMessage to replace {0} with property name
                    return new ValidationResult(
                        FormatErrorMessage(validationContext.DisplayName)
                    );
                }
            }
            else
            {
                // If attribute applied to wrong type
                throw new ValidationException(
                    string.Format(
                        ErrorMessages
                            .CustomAttributes
                            .FutureDate_InvalidProperty,
                        validationContext.DisplayName
                    )
                );
            }

            return ValidationResult.Success;
        }
    }
}
