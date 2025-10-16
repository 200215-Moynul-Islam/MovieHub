using Microsoft.EntityFrameworkCore;
using MovieHub.API.Constants;
using MovieHub.API.Data;
using MovieHub.API.Models.Base;

namespace MovieHub.API.Repositories
{
    public class NamedRepository<T> : Repository<T>, INamedRepository<T>
        where T : SoftDeletableNamedEntityBase
    {
        public NamedRepository(MovieHubDbContext dbContext)
            : base(dbContext) { }

        public async Task<bool> NameExistsCaseInsensitiveAsync(string name)
        {
            return await _dbSet.AnyAsync(e =>
                EF.Functions.Collate(e.Name, DatabaseCollations.CaseInsensitive) == name
            );
        }
    }
}
