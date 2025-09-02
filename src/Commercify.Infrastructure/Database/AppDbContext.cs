using Commercify.Core.Models;
using Commercify.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace Commercify.Infrastructure.Database;

public class AppDbContext : DbContext, IDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync()
    {
        var currentTime = DateTime.UtcNow;
        foreach(var entity in ChangeTracker.Entries<BaseEntity>())
        {
            switch(entity.State)
            {
                case EntityState.Added:
                    entity.Entity.CreatedAt = currentTime;
                    entity.Entity.LastUpdatedAt = currentTime;
                    break;
                case EntityState.Modified:
                    entity.Entity.LastUpdatedAt = currentTime;
                    break;
            }
        }
        return await base.SaveChangesAsync();
    }

    public new DbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
    {
        return base.Set<TEntity>();
    }
}
