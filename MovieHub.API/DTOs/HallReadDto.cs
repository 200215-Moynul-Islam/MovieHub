using System.ComponentModel.DataAnnotations;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class HallReadDto : HallDtoBase
    {
        [Required]
        public int? Id { get; set; }
    }
}
