using NServiceBus;
using Shared;

namespace EventManagementContextAPI;

public class EventHandlingService : IHandleMessages<ContentRemoved>
{
    private EventManagementDbContext _dbContext;

    public EventHandlingService(EventManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(ContentRemoved message, IMessageHandlerContext context)
    {
        Console.WriteLine("eventmanagement: ContentRemoved received");

        if (message.Type == "GroupEvent")
        {
            var groupEvent = await _dbContext.GroupEvents.FindAsync(message.ContentId);
            groupEvent.RemovedBySuperadmin = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
