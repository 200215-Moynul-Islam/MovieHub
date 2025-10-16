using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieHub.API.Constants;
using MovieHub.API.Models.Base;

namespace MovieHub.API.Models
{
    public class Hall : SoftDeletableNamedEntityBase
    {
        [Required]
        [Range(ValidationConstants.Hall.MinRows, ValidationConstants.Hall.MaxRows)]
        public int TotalRows { get; set; }

        [Required]
        [Range(ValidationConstants.Hall.MinColumns, ValidationConstants.Hall.MaxColumns)]
        public int TotalColumns { get; set; }

        [Required]
        public int BranchId { get; set; } // Foreign Key to Branch

        // Navigation Properties
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; } = null!; // Navigation property to related Branch

        public ICollection<Seat> Seats { get; set; } = new List<Seat>(); // Navigation property to related Seats

        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>(); // Navigation property to related Showtimes
    }
}
