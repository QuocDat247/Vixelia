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