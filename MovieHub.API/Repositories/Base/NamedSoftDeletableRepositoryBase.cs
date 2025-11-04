using Microsoft.EntityFrameworkCore;
using MovieHub.API.Constants;
using MovieHub.API.Data;
using MovieHub.API.Models.Base;

namespace MovieHub.API.Repositories.Base
{
    public abstract class NamedSoftDeletableRepositoryBase<T>
        : SoftDeletableRepositoryBase<T>,
            INamedSoftDeletableRepositoryBase<T>
        where T : NamedSoftDeletableEntityBase
    {
        public NamedSoftDeletableRepositoryBase(MovieHubDbContext dbContext)
            : base(dbContext) { }

        public async Task<bool> NameExistsCaseInsensitiveAsync(string name)
        {
            return await _dbSet.AnyAsync(e =>
                EF.Functions.Collate(e.Name, DatabaseCollations.CaseInsensitive)
                == name
            );
        }
    }
}
