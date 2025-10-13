using MovieHub.API.Constants;
using System.ComponentModel.DataAnnotations;

namespace MovieHub.API.DTOs.Base
{
    public abstract class BranchDtoBase : NamedDtoBase
    {
        private string _location = String.Empty;
        [Required(ErrorMessage = ErrorMessages.Branch.LocationRequired)]
        [MaxLength(ValidationConstants.Branch.MaxLocationLength, 
            ErrorMessage = ErrorMessages.Branch.LocationMaxLengthExceeded)]
        [RegularExpression(ValidationConstants.Branch.LocationRegex, 
            ErrorMessage = ErrorMessages.Branch.InvalidLocationFormat)]
        public string Location
        {
            get => _location;
            set => _location = value?.Trim() ?? String.Empty;
        }
    }
}
