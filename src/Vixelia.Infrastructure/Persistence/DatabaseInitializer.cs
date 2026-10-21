using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vixelia.Application.Abstractions.Persistence;
using Vixelia.Application.Abstractions.Storage;

namespace Vixelia.Infrastructure.Persistence;

internal sealed class DatabaseInitializer
    : IDatabaseInitializer
{
    private readonly IDbContextFactory<VixeliaDbContext>
        _dbContextFactory;

    private readonly IAppDataPaths _appDataPaths;

    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(
        IDbContextFactory<VixeliaDbContext> dbContextFactory,
        IAppDataPaths appDataPaths,
        ILogger<DatabaseInitializer> logger)
    {
        _dbContextFactory = dbContextFactory;
        _appDataPaths = appDataPaths;
        _logger = logger;
    }

    public async Task InitializeAsync(
        CancellationToken cancellationToken = default)
    {
        _appDataPaths.EnsureDirectoriesExist();

        await using var dbContext =
            await _dbContextFactory.CreateDbContextAsync(
                cancellationToken);

        await dbContext.Database.OpenConnectionAsync(
            cancellationToken);

        await dbContext.Database.CloseConnectionAsync();

        _logger.LogInformation(
            "Vixelia database initialized at {DatabasePath}.",
            _appDataPaths.DatabasePath);
    }
}