using MovieHub.API.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace MovieHub.API.DTOs
{
    public class BranchCreateDto : BranchDtoBase
    {
        public Guid? ManagerId { get; set; }
    }
}
