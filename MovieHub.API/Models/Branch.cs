using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;
using MovieHub.API.Models.Base;

namespace MovieHub.API.Models
{
    public class Branch : SoftDeletableNamedEntityBase
    {
        [Required]
        [MaxLength(ValidationConstants.Branch.MaxLocationLength)]
        public string Location { get; set; } = string.Empty;

        public Guid? ManagerId { get; set; } // Foreign Key to User (Manager)

        // Navigation Properties
        // [ForeignKey("ManagerId")]
        // public User? Manager { get; set; } // Navigation property to related User (Manager)
        public ICollection<Hall> Halls { get; set; } = new List<Hall>(); // Navigation property to related Halls
    }
}
