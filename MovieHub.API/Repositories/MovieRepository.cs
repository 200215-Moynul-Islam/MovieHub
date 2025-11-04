using Microsoft.EntityFrameworkCore;
using MovieHub.API.Constants;
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
            return await _dbSet.AnyAsync(e =>
                e.IsDeleted == false
                && EF.Functions.Collate(
                    e.Title,
                    DatabaseCollations.CaseInsensitive
                ) == title
            );
        }
    }
}
