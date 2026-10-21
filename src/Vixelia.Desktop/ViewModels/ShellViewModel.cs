using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace Vixelia.Desktop.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{
    private readonly ILogger<ShellViewModel> _logger;

    public ShellViewModel(ILogger<ShellViewModel> logger)
    {
        _logger = logger;

        _logger.LogInformation("Vixelia shell initialized.");
    }

    public string ApplicationName => "Vixelia";

    public string VersionText => "0.1.0-alpha.1";
}