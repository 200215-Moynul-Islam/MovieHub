using MovieHub.API.Models;
using MovieHub.API.Repositories.Base;

namespace MovieHub.API.Repositories
{
    public interface IMovieRepository : ISoftDeletableRepositoryBase<Movie>
    {
        Task<bool> TitleExistsCaseInsensitiveAsync(string title);
    }
}
