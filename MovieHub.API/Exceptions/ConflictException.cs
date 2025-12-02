namespace MovieHub.API.Exceptions
{
    public class ConflictException : BusinessException
    {
        public ConflictException(string message)
            : base(message) { }
    }
}
