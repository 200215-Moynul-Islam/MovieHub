using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public class ShowTimeRepository
        : RepositoryBase<ShowTime>,
            IShowTimeRepository
    {
        public ShowTimeRepository(MovieHubDbContext dbContext)
            : base(dbContext) { }

        public async Task<bool> HasAnyUpcomingShowTimesByMovieIdAsync(
            int movieId
        )
        {
            return await _dbSet.AnyAsync(e =>
                e.MovieId == movieId && e.StartTime >= DateTime.UtcNow
            );
        }

        public async Task<bool> HasAnyUpcomingShowTimesByHallIdAsync(int hallId)
        {
            return await _dbSet.AnyAsync(e =>
                e.HallId == hallId && e.StartTime >= DateTime.UtcNow
            );
        }

        public async Task<bool> HasAnyConflictingShowTimeInHallAsync(
            DateTime startTime,
            DateTime endTime,
            int hallId
        )
        {
            return await _dbSet
                .Include(st => st.Movie)
                .Where(st => st.HallId == hallId)
                .AnyAsync(st =>
                    !(
                        st.StartTime >= endTime
                        || st.StartTime.AddMinutes(
                            st.Movie.Duration + st.BufferMinutes
                        ) <= startTime
                    )
                );
        }

        public async Task<ShowTime?> GetShowTimeWithHallAndBookedSeatsAsync(
            int showTimeId
        )
        {
            return await _dbSet
                .Include(st => st.Hall)
                .ThenInclude(h => h.Seats)
                .Include(st => st.Bookings)
                .ThenInclude(b => b.BookedSeats)
                .Where(st => st.Id == showTimeId)
                .FirstOrDefaultAsync();
        }

        public async Task<ShowTime?> GetShowTimeDetailsByIdAsync(int id)
        {
            return await _dbSet
                .Include(st => st.Movie)
                .Include(st => st.Hall)
                .ThenInclude(h => h.Seats)
                .Include(st => st.Bookings)
                .ThenInclude(b => b.BookedSeats)
                .Where(st => st.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
