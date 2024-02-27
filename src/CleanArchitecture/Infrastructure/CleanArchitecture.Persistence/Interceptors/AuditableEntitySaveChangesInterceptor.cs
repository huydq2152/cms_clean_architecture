using CleanArchitecture.Application.Interfaces.Services.Auth.User;
using Contracts.Domains.Interfaces;
using Contracts.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanArchitecture.Persistence.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AuditableEntitySaveChangesInterceptor(
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService)
    {
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;
        var modified = context.ChangeTracker.Entries().Where(o =>
            o.State == EntityState.Added ||
            o.State == EntityState.Modified ||
            o.State == EntityState.Deleted ||
            o.HasChangedOwnedEntities());

        foreach (var item in modified)
        {
            switch (item.State)
            {
                case EntityState.Added:
                    if (item.Entity is ICreationAudited addedEntity)
                    {
                        addedEntity.CreationTime = _dateTimeService.Now;
                        addedEntity.CreatorUserId = _currentUserService.UserId;
                        item.State = EntityState.Added;
                    }

                    break;

                case EntityState.Modified:
                    context.Entry(item.Entity).Property("Id").IsModified = false;
                    if (item.Entity is IModificationAudited modifiedEntity)
                    {
                        modifiedEntity.LastModificationTime = _dateTimeService.Now;
                        modifiedEntity.LastModifiedUserId = _currentUserService.UserId;
                        item.State = EntityState.Modified;
                    }

                    break;

                case EntityState.Deleted:
                    if (item.Entity is IDeletionAudited deletedEntity)
                    {
                        deletedEntity.DeletionTime = _dateTimeService.Now;
                        deletedEntity.DeleterUserId = _currentUserService.UserId;
                        deletedEntity.IsDeleted = true;
                        item.State = EntityState.Deleted;
                    }

                    break;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}