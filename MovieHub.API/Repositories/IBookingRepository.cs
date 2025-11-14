using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public interface IBookingRepository : IRepositoryBase<Booking>
    {
        Task<IEnumerable<Booking>> GetAllBookingsWithSeatsByUserIdAsync(
            Guid userId,
            int offset,
            int limit
        );
    }
}
