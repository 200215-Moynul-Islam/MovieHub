using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieHub.API.Models
{
    public class Hall
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 20)]
        public int TotalRows { get; set; }

        [Required]
        [Range(1, 50)]
        public int TotalColumns { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public int BranchId { get; set; } // Foreign Key to Branch

        // Navigation Properties
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; } = null!; // Navigation property to related Branch

        public ICollection<Seat> Seats { get; set; } = new List<Seat>(); // Navigation property to related Seats

        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>(); // Navigation property to related Showtimes
    }
}
