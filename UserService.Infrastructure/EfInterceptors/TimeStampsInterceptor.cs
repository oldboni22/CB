using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserService.Domain.Data.Models;

namespace UserService.Infrastructure.EfInterceptors;

public class TimeStampsInterceptor(TimeProvider timeProvider) : ISaveChangesInterceptor
{
    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SetTimeStamps(eventData);
        
        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        SetTimeStamps(eventData);
        
        return new ValueTask<InterceptionResult<int>>(result);
    }

    private void SetTimeStamps(DbContextEventData eventData)
    {
        var entities = eventData!.Context!.ChangeTracker
            .Entries()
            .Where(entry => entry is
            {
                State: EntityState.Added or EntityState.Modified
            })
            .ToList();

        var currentTime = timeProvider.GetUtcNow().DateTime; 
        
        foreach (var entry in entities)
        {
            var entity = (EntityBase)entry.Entity;
            
            entity.UpdatedAt =  currentTime;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = currentTime;
            }
        }
    }
}