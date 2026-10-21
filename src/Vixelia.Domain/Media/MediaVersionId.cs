namespace Vixelia.Domain.Media;

public readonly record struct MediaVersionId
{
    public Guid Value { get; }

    private MediaVersionId(Guid value)
    {
        Value = value;
    }

    public static MediaVersionId New()
    {
        return new MediaVersionId(Guid.NewGuid());
    }

    public static MediaVersionId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(
                "Media version ID cannot be empty.",
                nameof(value));
        }

        return new MediaVersionId(value);
    }

    public bool IsEmpty => Value == Guid.Empty;

    public override string ToString()
    {
        return Value.ToString();
    }
}