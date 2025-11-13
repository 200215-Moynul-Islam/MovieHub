using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public interface IShowTimeRepository : IRepositoryBase<ShowTime>
    {
        Task<bool> HasAnyUpcomingShowTimesByMovieIdAsync(int movieId);
        Task<bool> HasAnyUpcomingShowTimesByHallIdAsync(int hallId);
        Task<bool> HasAnyConflictingShowTimeInHallAsync(
            DateTime startTime,
            DateTime endTime,
            int hallId
        );
        Task<bool> HasAnyConflictingShowTimeInHallAsync(
            DateTime startTime,
            DateTime endTime,
            int hallId,
            int currentShowTimeId
        );
        Task<ShowTime?> GetShowTimeWithHallAndBookedSeatsAsync(int showTimeId);
        Task<ShowTime?> GetShowTimeDetailsByIdAsync(int id);
    }
}
