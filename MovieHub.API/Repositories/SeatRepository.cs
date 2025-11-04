using MovieHub.API.Data;
using MovieHub.API.Models;

namespace MovieHub.API.Repositories
{
    public class SeatRepository : Repository<Seat>, ISeatRepository
    {
        public SeatRepository(MovieHubDbContext movieHubDbContext)
            : base(movieHubDbContext) { }
    }
}
