using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vixelia.Application.Abstractions.Persistence;
using Vixelia.Application.Abstractions.Storage;
using Vixelia.Infrastructure.Persistence.Repositories;
using Vixelia.Infrastructure.Persistence;
using Vixelia.Infrastructure.Storage;

namespace Vixelia.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddVixeliaInfrastructure(
        this IServiceCollection services)
    {
        services.AddSingleton<IAppDataPaths, LocalAppDataPaths>();

        services.AddDbContextFactory<VixeliaDbContext>(
            (serviceProvider, options) =>
            {
                var paths =
                    serviceProvider
                        .GetRequiredService<IAppDataPaths>();

                options.UseSqlite(
                    $"Data Source={paths.DatabasePath}");
            });

        services.AddSingleton<
            IProjectRepository,
            ProjectRepository>();

        services.AddSingleton<
            IDatabaseInitializer,
            DatabaseInitializer>();

        return services;
    }
}