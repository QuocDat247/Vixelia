using Vixelia.Domain.Projects;

namespace Vixelia.Domain.Media;

public sealed class MediaAsset
{
    public const int MaxDisplayNameLength = 255;

    private MediaAsset()
    {
    }

    private MediaAsset(
        MediaAssetId id,
        ProjectId projectId,
        string displayName,
        MediaType mediaType,
        DateTimeOffset createdAt)
    {
        Id = id;
        ProjectId = projectId;
        DisplayName = displayName;
        MediaType = mediaType;
        CreatedAt = createdAt;
    }

    public MediaAssetId Id { get; private set; }

    public ProjectId ProjectId { get; private set; }

    public string DisplayName { get; private set; } = string.Empty;

    public MediaType MediaType { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public static MediaAsset Create(
        ProjectId projectId,
        string displayName,
        MediaType mediaType,
        DateTimeOffset createdAt)
    {
        if (projectId.IsEmpty)
        {
            throw new ArgumentException(
                "Project ID cannot be empty.",
                nameof(projectId));
        }

        var normalizedName =
            NormalizeDisplayName(displayName);

        return new MediaAsset(
            MediaAssetId.New(),
            projectId,
            normalizedName,
            mediaType,
            createdAt);
    }

    private static string NormalizeDisplayName(
        string displayName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

        var normalizedName = displayName.Trim();

        if (normalizedName.Length > MaxDisplayNameLength)
        {
            throw new ArgumentException(
                $"Media display name cannot exceed {MaxDisplayNameLength} characters.",
                nameof(displayName));
        }

        return normalizedName;
    }
}