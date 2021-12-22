using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared;

namespace EventManagementContextAPI;

public class EventManagementDbContextInterceptor : SaveChangesInterceptor
{
    private EventPublishingService _eventPublishingService;

    public EventManagementDbContextInterceptor(EventPublishingService eventPublishingService)
    {
        _eventPublishingService = eventPublishingService;
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        var groupEvent = (GroupEvent)eventData.Context.ChangeTracker.Entries().First().Entity;

        if (groupEvent.DomainEvents.Any())
        {
            var groupEventCreated = groupEvent.DomainEvents[0];

            // the event was created in entity ctor, before id was generated
            if (groupEventCreated.Id == 0)
                groupEventCreated.Id = groupEvent.Id;

            await _eventPublishingService.NotifyGroupEventCreated(groupEventCreated);
            groupEvent.DomainEvents.Clear();
        }
        return result;
    }
}
