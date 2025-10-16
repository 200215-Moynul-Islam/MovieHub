using System.ComponentModel.DataAnnotations;
using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class BranchCreateDto : BranchDtoBase
    {
        public Guid? ManagerId { get; set; }
    }
}
