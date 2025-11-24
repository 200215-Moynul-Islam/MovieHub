using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(
            ValidationConstants.User.MaxUsernameLength,
            MinimumLength = ValidationConstants.User.MinUsernameLength
        )]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public bool? IsDeleted { get; set; } = false;

        // Navigation Properties
        public ICollection<UserRole> UserRoles { get; set; } =
            new List<UserRole>();

        public ICollection<Booking> Bookings { get; set; } =
            new List<Booking>();

        public Branch? Branch { get; set; }
    }
}
