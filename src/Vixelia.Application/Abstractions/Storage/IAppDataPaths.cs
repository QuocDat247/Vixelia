namespace Vixelia.Application.Abstractions.Storage;

public interface IAppDataPaths
{
    string RootDirectory { get; }

    string DataDirectory { get; }

    string DatabasePath { get; }

    string LogsDirectory { get; }

    string CacheDirectory { get; }

    void EnsureDirectoriesExist();
}