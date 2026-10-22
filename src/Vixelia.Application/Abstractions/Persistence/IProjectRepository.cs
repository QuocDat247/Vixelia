using Vixelia.Domain.Projects;

namespace Vixelia.Application.Abstractions.Persistence;

public interface IProjectRepository
{
    Task AddAsync(
        Project project,
        CancellationToken cancellationToken = default);

    Task<Project?> GetByIdAsync(
        ProjectId id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Project>> GetAllAsync(
        CancellationToken cancellationToken = default);
}