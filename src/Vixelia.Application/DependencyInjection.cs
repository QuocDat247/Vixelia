using Microsoft.Extensions.DependencyInjection;
using Vixelia.Application.Projects;

namespace Vixelia.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddVixeliaApplication(
        this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);

        services.AddSingleton<
            IProjectService,
            ProjectService>();

        return services;
    }
}