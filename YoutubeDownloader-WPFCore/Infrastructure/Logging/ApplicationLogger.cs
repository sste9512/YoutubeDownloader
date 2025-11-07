using SurrealDb.Net;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SurrealDb.Embedded.RocksDb;

namespace YoutubeDownloader_WPFCore.Infrastructure.Logging;

public sealed class ApplicationLogger : ILogger
{
    private readonly SurrealDbRocksDbClient _surrealDbRocksDbClient;
    private readonly string _categoryName;
    private const string LogTable = "logs";

    public ApplicationLogger(SurrealDbRocksDbClient surrealDbRocksDbClient, string categoryName = "Application")
    {
        _surrealDbRocksDbClient = surrealDbRocksDbClient ?? throw new ArgumentNullException(nameof(surrealDbRocksDbClient));
        _categoryName = categoryName;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        try
        {
            var message = formatter(state, exception);
            var exceptionMessage = exception?.ToString();
            
            // Fire and forget - log asynchronously without blocking
            _ = LogMessageAsync(logLevel.ToString(), message, exceptionMessage, eventId);
        }
        catch
        {
            // Suppress logging errors to prevent cascading failures
        }
    }

    public async Task LogInformation(string message)
    {
        await LogMessageAsync("INFO", message, null, default);
    }

    public async Task LogWarning(string message)
    {
        await LogMessageAsync("WARNING", message, null, default);
    }

    public async Task LogError(string message, string? exceptionMessage = null)
    {
        await LogMessageAsync("ERROR", message, exceptionMessage, default);
    }

    private async Task LogMessageAsync(string level, string message, string? exceptionMessage, EventId eventId)
    {
        try
        {
            var logEntry = new
            {
                timestamp = DateTime.UtcNow,
                level,
                category = _categoryName,
                message,
                exception = exceptionMessage,
                eventId = eventId.Id,
                eventName = eventId.Name
            };

            await _surrealDbRocksDbClient.Create(LogTable, logEntry);
        }
        catch
        {
            // Suppress logging errors to prevent cascading failures
        }
    }
}