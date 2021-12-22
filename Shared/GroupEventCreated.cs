using NServiceBus;

namespace Shared;

public class GroupEventCreated : IEvent
{
    public long Id { get; set; }
    public string Name { get; set; }

}
