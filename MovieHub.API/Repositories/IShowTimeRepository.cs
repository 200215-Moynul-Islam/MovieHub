using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public interface IShowTimeRepository : IRepositoryBase<ShowTime>
    {
        Task<bool> HasAnyUpcomingShowTimesByMovieIdAsync(int movieId);
        Task<bool> HasAnyUpcomingShowTimesByHallIdAsync(int hallId);
    }
}
