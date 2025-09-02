using Commercify.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Commercify.Core.Shared;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity;
    Task<int> SaveChangesAsync();
}
