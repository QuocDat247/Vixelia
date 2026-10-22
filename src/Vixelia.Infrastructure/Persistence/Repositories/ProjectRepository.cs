using Microsoft.EntityFrameworkCore;
using Vixelia.Application.Abstractions.Persistence;
using Vixelia.Domain.Projects;

namespace Vixelia.Infrastructure.Persistence.Repositories;

internal sealed class ProjectRepository
    : IProjectRepository
{
    private readonly IDbContextFactory<VixeliaDbContext>
        _dbContextFactory;

    public ProjectRepository(
        IDbContextFactory<VixeliaDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task AddAsync(
        Project project,
        CancellationToken cancellationToken = default)
    {
        await using var dbContext =
            await _dbContextFactory.CreateDbContextAsync(
                cancellationToken);

        dbContext.Projects.Add(project);

        await dbContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(
        ProjectId id,
        CancellationToken cancellationToken = default)
    {
        await using var dbContext =
            await _dbContextFactory.CreateDbContextAsync(
                cancellationToken);

        return await dbContext.Projects
            .AsNoTracking()
            .SingleOrDefaultAsync(
                project => project.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Project>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        await using var dbContext =
            await _dbContextFactory.CreateDbContextAsync(
                cancellationToken);

        var projects = await dbContext.Projects
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return projects
            .OrderByDescending(project => project.UpdatedAt)
            .ToArray();
    }
}