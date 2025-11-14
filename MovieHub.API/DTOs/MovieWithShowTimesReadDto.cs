namespace MovieHub.API.DTOs
{
    public class MovieWithShowTimesReadDto : MovieReadDto
    {
        public ICollection<ShowTimeReadDto> ShowTimes { get; set; } =
            new List<ShowTimeReadDto>();
    }
}
