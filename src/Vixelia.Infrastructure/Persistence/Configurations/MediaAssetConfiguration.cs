using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vixelia.Domain.Media;
using Vixelia.Domain.Projects;

namespace Vixelia.Infrastructure.Persistence.Configurations;

internal sealed class MediaAssetConfiguration
    : IEntityTypeConfiguration<MediaAsset>
{
    public void Configure(
        EntityTypeBuilder<MediaAsset> builder)
    {
        builder.ToTable("MediaAssets");

        builder.HasKey(asset => asset.Id);

        builder.Property(asset => asset.Id)
            .HasConversion(
                id => id.Value,
                value => MediaAssetId.From(value))
            .ValueGeneratedNever();

        builder.Property(asset => asset.ProjectId)
            .HasConversion(
                id => id.Value,
                value => ProjectId.From(value))
            .IsRequired();

        builder.Property(asset => asset.DisplayName)
            .HasMaxLength(MediaAsset.MaxDisplayNameLength)
            .IsRequired();

        builder.Property(asset => asset.MediaType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(asset => asset.CreatedAt)
            .IsRequired();

        builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(asset => asset.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(asset => asset.ProjectId);
    }
}