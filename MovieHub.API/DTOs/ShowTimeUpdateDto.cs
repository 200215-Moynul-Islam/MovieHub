using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;
using MovieHub.API.CustomAttributes;

namespace MovieHub.API.DTOs
{
    public class ShowTimeUpdateDto
    {
        [FutureDate]
        public DateTime? StartTime { get; set; }

        [Range(
            ValidationConstants.ShowTime.MinBufferMinutes,
            ValidationConstants.ShowTime.MaxBufferMinutes
        )]
        public int? BufferMinutes { get; set; }

        public int? MovieId { get; set; }

        public int? HallId { get; set; }
    }
}
