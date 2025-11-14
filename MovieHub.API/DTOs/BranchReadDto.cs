using System.ComponentModel.DataAnnotations;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class BranchReadDto : BranchDtoBase
    {
        [Required]
        public int? Id { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
