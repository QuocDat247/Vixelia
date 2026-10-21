using Microsoft.EntityFrameworkCore;

namespace Vixelia.Infrastructure.Persistence;

public sealed class VixeliaDbContext : DbContext
{
    public VixeliaDbContext(
        DbContextOptions<VixeliaDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}