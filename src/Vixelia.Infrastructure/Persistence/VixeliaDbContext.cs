using Microsoft.EntityFrameworkCore;
using Vixelia.Domain.Media;
using Vixelia.Domain.Projects;

namespace Vixelia.Infrastructure.Persistence;

public sealed class VixeliaDbContext : DbContext
{
    public VixeliaDbContext(
        DbContextOptions<VixeliaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<MediaAsset> MediaAssets =>
        Set<MediaAsset>();

    public DbSet<MediaVersion> MediaVersions =>
        Set<MediaVersion>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(VixeliaDbContext).Assembly);
    }
}