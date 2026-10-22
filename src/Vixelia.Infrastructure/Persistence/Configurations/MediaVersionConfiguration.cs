using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vixelia.Domain.Media;

namespace Vixelia.Infrastructure.Persistence.Configurations;

internal sealed class MediaVersionConfiguration
    : IEntityTypeConfiguration<MediaVersion>
{
    public void Configure(
        EntityTypeBuilder<MediaVersion> builder)
    {
        builder.ToTable("MediaVersions");

        builder.HasKey(version => version.Id);

        builder.Property(version => version.Id)
            .HasConversion(
                id => id.Value,
                value => MediaVersionId.From(value))
            .ValueGeneratedNever();

        builder.Property(version => version.MediaAssetId)
            .HasConversion(
                id => id.Value,
                value => MediaAssetId.From(value))
            .IsRequired();

        builder.Property(version => version.ParentVersionId)
            .HasConversion<Guid?>(
                id => id.HasValue
                    ? id.Value.Value
                    : null,
                value => value.HasValue
                    ? MediaVersionId.From(value.Value)
                    : null);

        builder.Property(version => version.SequenceNumber)
            .IsRequired();

        builder.Property(version => version.StorageKey)
            .IsRequired();

        builder.Property(version => version.Operation)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(version => version.Prompt);

        builder.Property(version => version.CreatedAt)
            .IsRequired();

        builder.Ignore(version => version.IsRoot);

        builder.HasOne<MediaAsset>()
            .WithMany()
            .HasForeignKey(version => version.MediaAssetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<MediaVersion>()
            .WithMany()
            .HasForeignKey(version => version.ParentVersionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(version =>
                new
                {
                    version.MediaAssetId,
                    version.SequenceNumber
                })
            .IsUnique();

        builder.HasIndex(version => version.StorageKey)
            .IsUnique();
    }
}