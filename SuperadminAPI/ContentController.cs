using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperadminContextAPI;

[Route("api/[controller]")]
[ApiController]
public class ContentController : ControllerBase
{
    private readonly SuperadminDbContext _dbContext;

    public ContentController(SuperadminDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Content>>> GetAllAsync()
    {
        var allContent = await _dbContext.Content.ToListAsync();
        return allContent;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Content>>> MarkRemovedAsync(long id)
    {
        var content = await _dbContext.Content.FindAsync(id);

        if (content is null)
            return NotFound();

        content.MarkRemoved();
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
