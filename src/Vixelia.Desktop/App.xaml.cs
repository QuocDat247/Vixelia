using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vixelia.Application.Abstractions.Persistence;
using Vixelia.Infrastructure;
using Vixelia.Desktop.ViewModels;

namespace Vixelia.Desktop;

public partial class App : System.Windows.Application
{
    private readonly IHost _host;

    public App()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Services.AddVixeliaInfrastructure();

        builder.Services.AddSingleton<ShellViewModel>();
        builder.Services.AddSingleton<MainWindow>();

        _host = builder.Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host.StartAsync()
            .GetAwaiter()
            .GetResult();

        var databaseInitializer =
            _host.Services
                .GetRequiredService<IDatabaseInitializer>();

                databaseInitializer
                    .InitializeAsync()
                    .GetAwaiter()
                    .GetResult();

        var logger = _host.Services.GetRequiredService<ILogger<App>>();

        logger.LogInformation("Vixelia desktop application started.");

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        var logger = _host.Services.GetRequiredService<ILogger<App>>();

        logger.LogInformation("Vixelia desktop application shutting down.");

        _host.StopAsync()
            .GetAwaiter()
            .GetResult();

        _host.Dispose();

        base.OnExit(e);
    }
}