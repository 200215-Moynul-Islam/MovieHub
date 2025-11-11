using System.ComponentModel.DataAnnotations;

namespace MovieHub.API.DTOs
{
    public class HallWithSeatsReadDto : HallReadDto
    {
        [Required]
        public ICollection<SeatReadDto>? Seats { get; set; }
    }
}
