namespace Vixelia.Domain.Projects;

public readonly record struct ProjectId
{
    public Guid Value { get; }

    private ProjectId(Guid value)
    {
        Value = value;
    }

    public static ProjectId New()
    {
        return new ProjectId(Guid.NewGuid());
    }

    public static ProjectId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(
                "Project ID cannot be empty.",
                nameof(value));
        }

        return new ProjectId(value);
    }

    public bool IsEmpty => Value == Guid.Empty;

    public override string ToString()
    {
        return Value.ToString();
    }
}