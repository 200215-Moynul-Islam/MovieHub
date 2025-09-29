using System.ComponentModel.DataAnnotations;

namespace MovieHub.API.DTOs
{
    public class BranchCreateDto
    {
        private string _name = String.Empty;
        [Required(ErrorMessage = "Branch name is required.")]
        [MaxLength(100, ErrorMessage = "Branch name cannot exceed 100 characters.")]
        [RegularExpression("^[A-Za-z][A-Za-z0-9 .,'&-]*$", 
            ErrorMessage = "Branch name must start with a letter and contain only letters, numbers, spaces, and . , ' - &.")]
        public string Name
        {
            get => _name;
            set => _name = value?.Trim() ?? String.Empty;
        }

        private string _location = String.Empty;
        [Required(ErrorMessage = "Location is required.")]
        [MaxLength(250, ErrorMessage = "Location cannot exceed 250 characters.")]
        [RegularExpression("^[A-Za-z0-9 ./,'&-]*$", 
            ErrorMessage = "Location can only contain letters, numbers, spaces, and . / , ' - &.")]
        public string Location
        {
            get => _location;
            set => _location = value?.Trim() ?? String.Empty;
        }
        
        public Guid? ManagerId { get; set; }
    }
}
