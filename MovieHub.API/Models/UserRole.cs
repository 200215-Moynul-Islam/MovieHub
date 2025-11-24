using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieHub.API.Models
{
    public class UserRole
    {
        // Composite Key: UserId + RoleId. Configure it in DbContext using Fluent API
        // because Data annotation has not this facility.)
        [Required]
        public Guid? UserId { get; set; }

        [Required]
        public int? RoleId { get; set; }

        // Navigation Properties
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("RoleId")]
        public Role Role { get; set; } = null!;
    }
}
