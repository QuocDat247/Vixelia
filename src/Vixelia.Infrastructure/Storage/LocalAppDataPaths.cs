using Vixelia.Application.Abstractions.Storage;

namespace Vixelia.Infrastructure.Storage;

internal sealed class LocalAppDataPaths : IAppDataPaths
{
    public LocalAppDataPaths()
    {
        RootDirectory = Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData),
            "Vixelia");

        DataDirectory = Path.Combine(
            RootDirectory,
            "Data");

        LogsDirectory = Path.Combine(
            RootDirectory,
            "Logs");

        CacheDirectory = Path.Combine(
            RootDirectory,
            "Cache");

        DatabasePath = Path.Combine(
            DataDirectory,
            "vixelia.db");
    }

    public string RootDirectory { get; }

    public string DataDirectory { get; }

    public string DatabasePath { get; }

    public string LogsDirectory { get; }

    public string CacheDirectory { get; }

    public void EnsureDirectoriesExist()
    {
        Directory.CreateDirectory(RootDirectory);
        Directory.CreateDirectory(DataDirectory);
        Directory.CreateDirectory(LogsDirectory);
        Directory.CreateDirectory(CacheDirectory);
    }
}