using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class BranchUpdateDto : NullableNamedDtoBase
    {
        private string? _location;

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
            set => _location = value?.Trim();
        }

        public Guid? ManagerId{ get; set; }
    }
}