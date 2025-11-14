using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.DTOs.Base
{
    public abstract class BookingDtoBase
    {
        [Required]
        public Guid? UserId { get; set; }

        [Required]
        public int? ShowTimeId { get; set; }
    }
}
