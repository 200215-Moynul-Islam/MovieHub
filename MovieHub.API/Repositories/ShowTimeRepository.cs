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
    }
}
