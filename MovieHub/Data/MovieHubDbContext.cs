using Microsoft.EntityFrameworkCore;

namespace MovieHub.API.Data
{
    public class MovieHubDbContext : DbContext
    {
        public MovieHubDbContext(DbContextOptions<MovieHubDbContext> options) : base(options)
        {
        }
    }
}
