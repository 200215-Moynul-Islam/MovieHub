using MovieHub.API.DTOs.Base;

namespace MovieHub.API.DTOs
{
    public class UserReadDto : UserDtoBase
    {
        public Guid Id { get; set; }
    }
}
