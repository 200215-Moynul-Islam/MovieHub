namespace MovieHub.API.DTOs
{
    public class BranchReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public Guid? ManagerId { get; set; }
    }
}
