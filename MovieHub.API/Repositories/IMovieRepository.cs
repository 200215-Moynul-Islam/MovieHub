using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public interface IMovieRepository : ISoftDeletableRepositoryBase<Movie>
    {
        Task<bool> TitleExistsCaseInsensitiveAsync(string title);
        Task<int?> GetMovieDurationByMovieIdAsync(int movieId);
        Task<IEnumerable<Movie>> GetScheduledMoviesByBranchIdAsync(
            int branchId
        );
    }
}
