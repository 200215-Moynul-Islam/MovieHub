using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.Models.Base
{
    public abstract class SoftDeletableNamedEntityBase : EntityBase
    {
        [Required]
        [MaxLength(ValidationConstants.MaxNameLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
