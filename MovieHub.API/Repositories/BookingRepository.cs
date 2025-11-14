using Microsoft.EntityFrameworkCore;
using MovieHub.API.Data;
using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(MovieHubDbContext dbContext)
            : base(dbContext) { }

        public async Task<
            IEnumerable<Booking>
        > GetAllBookingsWithSeatsByUserIdAsync(
            Guid userId,
            int offset,
            int limit
        )
        {
            return await _dbSet
                .Include(b => b.BookedSeats)
                .Where(b => b.UserId == userId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }
    }
}
