using Vixelia.Application.Abstractions.Persistence;
using Vixelia.Domain.Projects;

namespace Vixelia.Application.Projects;

internal sealed class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly TimeProvider _timeProvider;

    public ProjectService(
        IProjectRepository projectRepository,
        TimeProvider timeProvider)
    {
        _projectRepository = projectRepository;
        _timeProvider = timeProvider;
    }

    public async Task<ProjectDto> CreateAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        var now = _timeProvider.GetUtcNow();

        var project = Project.Create(
            name,
            now);

        await _projectRepository.AddAsync(
            project,
            cancellationToken);

        return Map(project);
    }

    public async Task<ProjectDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            return null;
        }

        var project = await _projectRepository.GetByIdAsync(
            ProjectId.From(id),
            cancellationToken);

        return project is null
            ? null
            : Map(project);
    }

    public async Task<IReadOnlyList<ProjectDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var projects =
            await _projectRepository.GetAllAsync(
                cancellationToken);

        return projects
            .Select(Map)
            .ToArray();
    }

    private static ProjectDto Map(Project project)
    {
        return new ProjectDto(
            project.Id.Value,
            project.Name,
            project.CreatedAt,
            project.UpdatedAt);
    }
}