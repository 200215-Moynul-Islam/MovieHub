using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.Models.Base
{
    public abstract class NamedSoftDeletableEntityBase : SoftDeletableEntityBase
    {
        [Required]
        [MaxLength(ValidationConstants.MaxNameLength)]
        public string? Name { get; set; } = string.Empty;
    }
}
