using NServiceBus;

namespace Shared;

public class ContentRemoved : IEvent
{
    public long ContentId { get; set; }

    public string Type { get; set; }

}
