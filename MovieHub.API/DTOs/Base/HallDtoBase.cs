using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.DTOs.Base
{
    public abstract class HallDtoBase : NamedDtoBase
    {
        [Required]
        [Range(
            ValidationConstants.Hall.MinRows,
            ValidationConstants.Hall.MaxRows
        )]
        public int? TotalRows { get; set; }

        [Required]
        [Range(
            ValidationConstants.Hall.MinColumns,
            ValidationConstants.Hall.MaxColumns
        )]
        public int? TotalColumns { get; set; }

        [Required]
        public int? BranchId { get; set; }
    }
}
