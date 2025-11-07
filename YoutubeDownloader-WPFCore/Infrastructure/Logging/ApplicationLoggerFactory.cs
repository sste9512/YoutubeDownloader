using Microsoft.Extensions.Logging;
using SurrealDb.Embedded.RocksDb;

namespace YoutubeDownloader_WPFCore.Infrastructure.Logging;

public sealed class ApplicationLoggerFactory : ILoggerFactory
{
    private readonly ApplicationLoggerProvider _provider;
    private bool _disposed;

    public ApplicationLoggerFactory(SurrealDbRocksDbClient surrealDbRocksDbClient)
    {
        _provider = new ApplicationLoggerProvider(surrealDbRocksDbClient);
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(ApplicationLoggerFactory));
        }

        return _provider.CreateLogger(categoryName);
    }

    public void AddProvider(ILoggerProvider provider)
    {
        // Not supporting additional providers in this implementation
        // Could be extended if needed
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _provider.Dispose();
        _disposed = true;
    }
}

