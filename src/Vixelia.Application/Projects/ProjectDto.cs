namespace Vixelia.Application.Projects;

public sealed record ProjectDto(
    Guid Id,
    string Name,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt);