namespace Vixelia.Domain.Projects;

public sealed class Project
{
    public const int MaxNameLength = 120;

    private Project()
    {
    }

    private Project(
        ProjectId id,
        string name,
        DateTimeOffset createdAt)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = createdAt;
    }

    public ProjectId Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset UpdatedAt { get; private set; }

    public static Project Create(
        string name,
        DateTimeOffset createdAt)
    {
        var normalizedName = NormalizeName(name);

        return new Project(
            ProjectId.New(),
            normalizedName,
            createdAt);
    }

    public void Rename(
        string name,
        DateTimeOffset updatedAt)
    {
        if (updatedAt < UpdatedAt)
        {
            throw new ArgumentOutOfRangeException(
                nameof(updatedAt),
                "Updated time cannot be earlier than the current updated time.");
        }

        Name = NormalizeName(name);
        UpdatedAt = updatedAt;
    }

    private static string NormalizeName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        var normalizedName = name.Trim();

        if (normalizedName.Length > MaxNameLength)
        {
            throw new ArgumentException(
                $"Project name cannot exceed {MaxNameLength} characters.",
                nameof(name));
        }

        return normalizedName;
    }
}