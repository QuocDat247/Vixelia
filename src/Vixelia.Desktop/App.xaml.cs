using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vixelia.Application;
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

        builder.Services.AddVixeliaApplication();
        builder.Services.AddVixeliaInfrastructure();

        builder.Services.AddSingleton<ShellViewModel>();
        builder.Services.AddSingleton<MainWindow>();

        _host = builder.Build();
    }

    protected override async void OnStartup(
        StartupEventArgs e)
    {
        base.OnStartup(e);

        await _host.StartAsync();

        var databaseInitializer =
            _host.Services
                .GetRequiredService<IDatabaseInitializer>();

        await databaseInitializer.InitializeAsync();

        var shellViewModel =
            _host.Services
                .GetRequiredService<ShellViewModel>();

        await shellViewModel.InitializeAsync();

        var logger =
            _host.Services
                .GetRequiredService<ILogger<App>>();

        logger.LogInformation(
            "Vixelia desktop application started.");

        var mainWindow =
            _host.Services
                .GetRequiredService<MainWindow>();

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