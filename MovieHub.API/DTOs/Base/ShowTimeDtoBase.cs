using System.ComponentModel.DataAnnotations;
using MovieHub.API.Constants;

namespace MovieHub.API.DTOs.Base
{
    public abstract class ShowTimeDtoBase
    {
        [Required]
        public DateTime? StartTime { get; set; }

        [Required]
        [Range(
            ValidationConstants.ShowTime.MinBufferMinutes,
            ValidationConstants.ShowTime.MaxBufferMinutes,
            ErrorMessage = ErrorMessages.ShowTime.BufferMinutesOutOfRange
        )]
        public int? BufferMinutes { get; set; }

        [Required]
        public int? MovieId { get; set; } // Foreign Key to Movie

        [Required]
        public int? HallId { get; set; } // Foreign Key to Hall
    }
}
