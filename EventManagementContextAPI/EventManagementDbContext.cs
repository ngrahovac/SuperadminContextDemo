using Microsoft.EntityFrameworkCore;

namespace EventManagementContextAPI;

public class EventManagementDbContext : DbContext
{
    public DbSet<GroupEvent> GroupEvents { get; set; }

    public EventManagementDbContext(DbContextOptions<EventManagementDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*modelBuilder.Entity<GroupEvent>()
                    .Property<bool>("RemovedBySuperadmin");*/
    }
}
 