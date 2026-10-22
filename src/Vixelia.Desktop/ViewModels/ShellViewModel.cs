using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Vixelia.Application.Projects;

namespace Vixelia.Desktop.ViewModels;

public sealed partial class ShellViewModel : ObservableObject
{
    private readonly IProjectService _projectService;
    private readonly ILogger<ShellViewModel> _logger;

    public ShellViewModel(
        IProjectService projectService,
        ILogger<ShellViewModel> logger)
    {
        _projectService = projectService;
        _logger = logger;
    }

    public string ApplicationName => "Vixelia";

    public string VersionText => "0.1.0-alpha.1";

    public ObservableCollection<ProjectDto> Projects { get; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CreateProjectCommand))]
    private string _newProjectName = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CreateProjectCommand))]
    private bool _isBusy;

    [ObservableProperty]
    private string? _statusMessage;

    public async Task InitializeAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            IsBusy = true;
            StatusMessage = "Loading projects...";

            var projects =
                await _projectService.GetAllAsync(
                    cancellationToken);

            Projects.Clear();

            foreach (var project in projects)
            {
                Projects.Add(project);
            }

            StatusMessage = Projects.Count == 0
                ? "Create your first Vixelia project."
                : $"{Projects.Count} project(s) loaded.";

            _logger.LogInformation(
                "Loaded {ProjectCount} Vixelia projects.",
                Projects.Count);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand(CanExecute = nameof(CanCreateProject))]
    private async Task CreateProjectAsync()
    {
        try
        {
            IsBusy = true;
            StatusMessage = "Creating project...";

            var project =
                await _projectService.CreateAsync(
                    NewProjectName);

            Projects.Insert(0, project);

            NewProjectName = string.Empty;

            StatusMessage =
                $"Project '{project.Name}' created.";

            _logger.LogInformation(
                "Created project {ProjectId}.",
                project.Id);
        }
        catch (Exception exception)
        {
            StatusMessage = "Could not create the project.";

            _logger.LogError(
                exception,
                "Failed to create Vixelia project.");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private bool CanCreateProject()
    {
        return !IsBusy
            && !string.IsNullOrWhiteSpace(NewProjectName);
    }
}