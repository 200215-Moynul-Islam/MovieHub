namespace MovieHub.API.DTOs.Responses
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; } // Store general message(Succesful or Failed)
        public object? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
