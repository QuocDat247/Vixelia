namespace Vixelia.Application.Projects;

public interface IProjectService
{
    Task<ProjectDto> CreateAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task<ProjectDto?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProjectDto>> GetAllAsync(
        CancellationToken cancellationToken = default);
}