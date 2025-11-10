using System.ComponentModel.DataAnnotations;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class BookingReadDto : BookingDtoBase
    {
        [Required]
        public int Id { get; set; }
    }
}
