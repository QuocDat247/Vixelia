using System.Xml.Linq;

namespace Vixelia.Tests.Unit.Architecture;

public sealed class DependencyRulesTests
{
    [Fact]
    public void Domain_Should_Not_Reference_Outer_Layers()
    {
        var repositoryRoot = FindRepositoryRoot();

        var projectPath = Path.Combine(
            repositoryRoot,
            "src",
            "Vixelia.Domain",
            "Vixelia.Domain.csproj");

        var document = XDocument.Load(projectPath);

        var projectReferences = document
            .Descendants("ProjectReference")
            .Select(element => element.Attribute("Include")?.Value)
            .Where(reference => reference is not null)
            .ToArray();

        Assert.DoesNotContain(
            projectReferences,
            reference => reference!.Contains("Vixelia.Application"));

        Assert.DoesNotContain(
            projectReferences,
            reference => reference!.Contains("Vixelia.Infrastructure"));

        Assert.DoesNotContain(
            projectReferences,
            reference => reference!.Contains("Vixelia.Infrastructure.AI"));

        Assert.DoesNotContain(
            projectReferences,
            reference => reference!.Contains("Vixelia.Api"));

        Assert.DoesNotContain(
            projectReferences,
            reference => reference!.Contains("Vixelia.Desktop"));
    }

    [Fact]
    public void Domain_Namespaces_Should_Not_Be_Defined_In_Outer_Projects()
    {
        var repositoryRoot = FindRepositoryRoot();
        var sourceRoot = Path.Combine(repositoryRoot, "src");
        var domainRoot = Path.GetFullPath(
            Path.Combine(sourceRoot, "Vixelia.Domain"));

        var violations = Directory
            .EnumerateFiles(
                sourceRoot,
                "*.cs",
                SearchOption.AllDirectories)
            .Where(path => !IsBuildArtifact(path))
            .Where(path =>
                !Path.GetFullPath(path).StartsWith(
                    domainRoot + Path.DirectorySeparatorChar,
                    StringComparison.OrdinalIgnoreCase))
            .Where(path =>
                File.ReadAllText(path)
                    .Contains(
                        "namespace Vixelia.Domain",
                        StringComparison.Ordinal))
            .Select(path =>
                Path.GetRelativePath(repositoryRoot, path))
            .ToArray();

        Assert.Empty(violations);
    }

    private static bool IsBuildArtifact(string path)
    {
        var separator = Path.DirectorySeparatorChar;

        return path.Contains(
                   $"{separator}bin{separator}",
                   StringComparison.OrdinalIgnoreCase)
            || path.Contains(
                   $"{separator}obj{separator}",
                   StringComparison.OrdinalIgnoreCase);
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "Vixelia.sln")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException(
            "Could not locate the Vixelia repository root.");
    }
}