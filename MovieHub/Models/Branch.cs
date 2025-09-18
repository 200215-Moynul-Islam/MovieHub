using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieHub.API.Models
{
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(250)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(256)]
        public string? ManagerEmail { get; set; } // Foreign Key to User (Manager)

        // Navigation Properties
        // [ForeignKey("ManagerEmail")]
        // public User Manager { get; set; } = null!; // Navigation property to related User (Manager)
        public ICollection<Hall> Halls { get; set; } = new List<Hall>(); // Navigation property to related Halls
    }
}
