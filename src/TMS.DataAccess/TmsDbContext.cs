using Microsoft.EntityFrameworkCore;

using TMS.DataAccess.Entities.Task;

namespace TMS.DataAccess;

public class TmsDbContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }

    public TmsDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}