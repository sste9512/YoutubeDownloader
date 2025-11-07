using Microsoft.Extensions.Logging;
using SurrealDb.Embedded.RocksDb;
using System.Collections.Concurrent;

namespace YoutubeDownloader_WPFCore.Infrastructure.Logging;

public sealed class ApplicationLoggerProvider : ILoggerProvider
{
    private readonly SurrealDbRocksDbClient _surrealDbRocksDbClient;
    private readonly ConcurrentDictionary<string, ApplicationLogger> _loggers = new();
    private bool _disposed;

    public ApplicationLoggerProvider(SurrealDbRocksDbClient surrealDbRocksDbClient)
    {
        _surrealDbRocksDbClient = surrealDbRocksDbClient ?? throw new ArgumentNullException(nameof(surrealDbRocksDbClient));
    }

    public ILogger CreateLogger(string categoryName)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(ApplicationLoggerProvider));
        }

        return _loggers.GetOrAdd(categoryName, name => new ApplicationLogger(_surrealDbRocksDbClient, name));
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _loggers.Clear();
        _disposed = true;
    }
}

