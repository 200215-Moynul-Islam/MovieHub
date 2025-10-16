using System.ComponentModel.DataAnnotations;

namespace MovieHub.API.Models.Base
{
    public abstract class SoftDeletableEntityBase : EntityBase
    {
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
