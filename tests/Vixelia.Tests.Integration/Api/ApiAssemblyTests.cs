namespace Vixelia.Tests.Integration.Api;

public sealed class ApiAssemblyTests
{
    [Fact]
    public void Api_Project_Should_Be_Available()
    {
        var projectPath = FindRepositoryFile(
            "src",
            "Vixelia.Api",
            "Vixelia.Api.csproj");

        Assert.True(File.Exists(projectPath));
    }

    private static string FindRepositoryFile(params string[] pathParts)
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            var solutionPath = Path.Combine(directory.FullName, "Vixelia.sln");

            if (File.Exists(solutionPath))
            {
                return Path.Combine(
                    new[] { directory.FullName }
                        .Concat(pathParts)
                        .ToArray());
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException(
            "Could not locate the Vixelia repository root.");
    }
}
