using Microsoft.EntityFrameworkCore;

namespace CleanControllers.API.Data.DatabaseContexts;

public sealed class CleanControllersDbContext(DbContextOptions<CleanControllersDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanControllersDbContext).Assembly);
    }
}
