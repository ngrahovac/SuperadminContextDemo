using NServiceBus;
using Shared;

namespace EventManagementContextAPI;

public class EventPublishingService
{
    private readonly IMessageSession _messageSession;

    public EventPublishingService(IMessageSession messageSession)
    {
        _messageSession = messageSession;
    }

    public async Task NotifyGroupEventCreated(GroupEventCreated groupEventCreated)
    {
        await _messageSession.Publish(groupEventCreated);
        Console.WriteLine("eventmanagement: publishing GroupEventCreated");
    }
}
