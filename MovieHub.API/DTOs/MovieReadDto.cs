using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class MovieReadDto : MovieDtoBase
    {
        [Required]
        public int? Id { get; set; }
    }
}
