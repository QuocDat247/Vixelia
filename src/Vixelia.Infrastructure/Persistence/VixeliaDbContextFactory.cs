using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Vixelia.Infrastructure.Storage;

namespace Vixelia.Infrastructure.Persistence;

public sealed class VixeliaDbContextFactory
    : IDesignTimeDbContextFactory<VixeliaDbContext>
{
    public VixeliaDbContext CreateDbContext(
        string[] args)
    {
        var paths = new LocalAppDataPaths();

        paths.EnsureDirectoriesExist();

        var optionsBuilder =
            new DbContextOptionsBuilder<VixeliaDbContext>();

        optionsBuilder.UseSqlite(
            $"Data Source={paths.DatabasePath}");

        return new VixeliaDbContext(
            optionsBuilder.Options);
    }
}