using System.ComponentModel.DataAnnotations;

namespace MovieHub.API.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        // Navigation Properties
        public ICollection<UserRole> UserRoles { get; set; } =
            new List<UserRole>();
    }
}
