using NServiceBus;
using Shared;

namespace SuperadminContextAPI;

public class EventHandlingService : IHandleMessages<GroupEventCreated>
{
    private SuperadminDbContext _dbContext;

    public EventHandlingService(SuperadminDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(GroupEventCreated message, IMessageHandlerContext context)
    {
        Console.WriteLine("superadmin: GroupEventCreated received");

        var textContent = new string[] { message.Name };
        var content = new Content(message.Id, "GroupEvent", textContent);
        await _dbContext.Content.AddAsync(content);
        await _dbContext.SaveChangesAsync();
    }
}
