using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagementContextAPI;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly EventManagementDbContext _dbContext;

    public EventsController(EventManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<GroupEvent>>> GetAllAsync()
    {
        var allEvents = await _dbContext.GroupEvents.ToListAsync();
        return allEvents;
    }

    [HttpPost]
    public async Task<ActionResult> CreateEventAsync(string name)
    {
        var groupEvent = new GroupEvent(name);
        await _dbContext.GroupEvents.AddAsync(groupEvent);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
