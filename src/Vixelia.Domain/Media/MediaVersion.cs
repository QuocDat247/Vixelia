namespace Vixelia.Domain.Media;

public sealed class MediaVersion
{
    private MediaVersion()
    {
    }

    private MediaVersion(
        MediaVersionId id,
        MediaAssetId mediaAssetId,
        MediaVersionId? parentVersionId,
        int sequenceNumber,
        string storageKey,
        MediaOperation operation,
        string? prompt,
        DateTimeOffset createdAt)
    {
        Id = id;
        MediaAssetId = mediaAssetId;
        ParentVersionId = parentVersionId;
        SequenceNumber = sequenceNumber;
        StorageKey = storageKey;
        Operation = operation;
        Prompt = prompt;
        CreatedAt = createdAt;
    }

    public MediaVersionId Id { get; private set; }

    public MediaAssetId MediaAssetId { get; private set; }

    public MediaVersionId? ParentVersionId { get; private set; }

    public int SequenceNumber { get; private set; }

    public string StorageKey { get; private set; } = string.Empty;

    public MediaOperation Operation { get; private set; }

    public string? Prompt { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public bool IsRoot => ParentVersionId is null;

    public static MediaVersion CreateRoot(
        MediaAssetId mediaAssetId,
        string storageKey,
        MediaOperation operation,
        DateTimeOffset createdAt)
    {
        ValidateMediaAssetId(mediaAssetId);

        var normalizedStorageKey =
            NormalizeStorageKey(storageKey);

        if (operation is not MediaOperation.Import
            and not MediaOperation.Generate)
        {
            throw new ArgumentException(
                "A root media version must originate from an import or generation.",
                nameof(operation));
        }

        return new MediaVersion(
            MediaVersionId.New(),
            mediaAssetId,
            parentVersionId: null,
            sequenceNumber: 0,
            normalizedStorageKey,
            operation,
            prompt: null,
            createdAt);
    }

    public static MediaVersion CreateDerived(
        MediaAssetId mediaAssetId,
        MediaVersionId parentVersionId,
        int sequenceNumber,
        string storageKey,
        MediaOperation operation,
        string? prompt,
        DateTimeOffset createdAt)
    {
        ValidateMediaAssetId(mediaAssetId);

        if (parentVersionId.IsEmpty)
        {
            throw new ArgumentException(
                "Parent version ID cannot be empty.",
                nameof(parentVersionId));
        }

        if (sequenceNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(sequenceNumber),
                "Derived versions must have a sequence number greater than zero.");
        }

        var normalizedStorageKey =
            NormalizeStorageKey(storageKey);

        return new MediaVersion(
            MediaVersionId.New(),
            mediaAssetId,
            parentVersionId,
            sequenceNumber,
            normalizedStorageKey,
            operation,
            NormalizePrompt(prompt),
            createdAt);
    }

    private static void ValidateMediaAssetId(
        MediaAssetId mediaAssetId)
    {
        if (mediaAssetId.IsEmpty)
        {
            throw new ArgumentException(
                "Media asset ID cannot be empty.",
                nameof(mediaAssetId));
        }
    }

    private static string NormalizeStorageKey(
        string storageKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(storageKey);

        return storageKey.Trim();
    }

    private static string? NormalizePrompt(string? prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
        {
            return null;
        }

        return prompt.Trim();
    }
}