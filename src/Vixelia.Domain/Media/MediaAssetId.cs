namespace Vixelia.Domain.Media;

public readonly record struct MediaAssetId
{
    public Guid Value { get; }

    private MediaAssetId(Guid value)
    {
        Value = value;
    }

    public static MediaAssetId New()
    {
        return new MediaAssetId(Guid.NewGuid());
    }

    public static MediaAssetId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(
                "Media asset ID cannot be empty.",
                nameof(value));
        }

        return new MediaAssetId(value);
    }

    public bool IsEmpty => Value == Guid.Empty;

    public override string ToString()
    {
        return Value.ToString();
    }
}