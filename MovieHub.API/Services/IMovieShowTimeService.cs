namespace MovieHub.API.Services
{
    public interface IMovieShowTimeService
    {
        Task DeactivateMovieByIdAsync(int id);
    }
}
