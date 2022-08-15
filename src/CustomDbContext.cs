using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore;

public class CustomDbContext : DbContext
{
    public virtual DateTime CreatedAt => DateTime.Now;
    public virtual DateTime UpdatedAt => DateTime.Now;

    public CustomDbContext() { }
    public CustomDbContext(DbContextOptions options) : base(options) { }

    private IEnumerable<EntityEntry> GetEntityEntries()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is CustomBaseEntity && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified));
        return entries;
    }

    private void SetCurrentDateOnEachEntity()
    {
        foreach (var entityEntry in GetEntityEntries())
        {
            ((CustomBaseEntity)entityEntry.Entity).UpdatedAt = UpdatedAt;

            if (entityEntry.State == EntityState.Added)
            {
                ((CustomBaseEntity)entityEntry.Entity).CreatedAt = CreatedAt;
            }
        }
    }

    public override int SaveChanges()
    {
        SetCurrentDateOnEachEntity();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetCurrentDateOnEachEntity();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        SetCurrentDateOnEachEntity();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetCurrentDateOnEachEntity();
        return base.SaveChangesAsync(cancellationToken);
    }
}
