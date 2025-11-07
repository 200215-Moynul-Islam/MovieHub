using System.ComponentModel.DataAnnotations;
using MovieHub.API.CustomAttributes;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class ShowTimeCreateDto : ShowTimeDtoBase
    {
        [Required]
        [FutureDate]
        public new DateTime? StartTime { get; set; }
    }
}
