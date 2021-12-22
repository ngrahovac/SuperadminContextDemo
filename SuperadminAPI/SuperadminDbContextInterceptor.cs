using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SuperadminContextAPI;

public class SuperadminDbContextInterceptor : SaveChangesInterceptor
{
    private EventPublishingService _eventPublishingService;

    public SuperadminDbContextInterceptor(EventPublishingService eventPublishingService)
    {
        _eventPublishingService = eventPublishingService;
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        var content = (Content)eventData.Context.ChangeTracker.Entries().First().Entity;
        if (content.DomainEvents.Any())
        {
            var contentRemoved = content.DomainEvents[0];
            await _eventPublishingService.NotifyContentRemoved(contentRemoved);
            content.DomainEvents.Clear();
        }
        return result;
    }
}
