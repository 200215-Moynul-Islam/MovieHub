using MovieHub.API.Data;
using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public class MovieRepository
        : SoftDeletableRepositoryBase<Movie>,
            IMovieRepository
    {
        public MovieRepository(MovieHubDbContext dbContext)
            : base(dbContext) { }

        public async Task<bool> TitleExistsCaseInsensitiveAsync(string title)
        {
            // TODO: Implement the method.
            return false;
        }
    }
}
