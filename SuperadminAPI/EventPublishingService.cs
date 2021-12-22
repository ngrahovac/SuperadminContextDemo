using NServiceBus;
using Shared;

namespace SuperadminContextAPI;

public class EventPublishingService
{
    private IMessageSession _messageSession;

    public EventPublishingService(IMessageSession messageSession)
    {
        _messageSession = messageSession;
    }

    public async Task NotifyContentRemoved(ContentRemoved contentRemoved)
    {
        await _messageSession.Publish(contentRemoved);
        Console.WriteLine("superadmin: publishing ContentRemoved");
    }
}
