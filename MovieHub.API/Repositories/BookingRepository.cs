using MovieHub.API.Data;
using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(MovieHubDbContext dbContext)
            : base(dbContext) { }
    }
}
