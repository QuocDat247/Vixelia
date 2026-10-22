using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vixelia.Domain.Projects;

namespace Vixelia.Infrastructure.Persistence.Configurations;

internal sealed class ProjectConfiguration
    : IEntityTypeConfiguration<Project>
{
    public void Configure(
        EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(project => project.Id);

        builder.Property(project => project.Id)
            .HasConversion(
                id => id.Value,
                value => ProjectId.From(value))
            .ValueGeneratedNever();

        builder.Property(project => project.Name)
            .HasMaxLength(Project.MaxNameLength)
            .IsRequired();

        builder.Property(project => project.CreatedAt)
            .IsRequired();

        builder.Property(project => project.UpdatedAt)
            .IsRequired();
    }
}