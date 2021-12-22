using Microsoft.EntityFrameworkCore;

namespace SuperadminContextAPI;

public class SuperadminDbContext : DbContext
{
    public DbSet<Content> Content { get; set; }

    public SuperadminDbContext(DbContextOptions<SuperadminDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Content>()
                    .Property(c => c.TextContent)
                    .HasConversion(arr => string.Join(",", arr),
                                   str => str.Split(",", StringSplitOptions.RemoveEmptyEntries));
    }
}
