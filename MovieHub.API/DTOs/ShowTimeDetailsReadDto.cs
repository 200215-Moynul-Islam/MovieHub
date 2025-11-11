using System.ComponentModel.DataAnnotations;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class ShowTimeDetailsReadDto : ShowTimeDtoBase
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public MovieReadDto? Movie { get; set; }

        [Required]
        public HallWithSeatsReadDto? Hall { get; set; }

        public ICollection<BookingReadDto> Bookings { get; set; } = [];
    }
}
