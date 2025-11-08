using System.Data;
using Microsoft.Extensions.Logging;
using SurrealDb.Embedded.RocksDb;
using SurrealDb.Net;
using SurrealDb.Net.Handlers;
using YoutubeDownloader_WPFCore.Application.Interfaces;
using YoutubeDownloader_WPFCore.Infrastructure.Values;

namespace YoutubeDownloader_WPFCore.Infrastructure.Database;

public sealed class SurrealDocumentDb(SurrealDbRocksDbClient rocksDbClient, ILogger<SurrealDocumentDb> logger) : IDocumentDb
{
    private readonly SurrealDbRocksDbClient _rocksDbClient = rocksDbClient ?? throw new ArgumentNullException(nameof(rocksDbClient));
    private readonly ILogger<SurrealDocumentDb> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    /// <summary>
    /// Connect to SurrealDB    
    /// </summary>
    /// <param name="url">The URL of the SurrealDB instance</param>
    /// <param name="username">The username to connect to the SurrealDB instance</param>
    /// <param name="password">The password to connect to the SurrealDB instance</param>
    /// <param name="namespace_">The namespace to connect to the SurrealDB instance</param>
    /// <param name="database">The database to connect to the SurrealDB instance</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation</returns>
    public async Task<bool> ConnectAsync(string url, string username, string password, string namespace_, string database)
    {
        try
        {
            _logger.LogInformation("Connecting to SurrealDB with namespace: {Namespace}, database: {Database}", namespace_, database);
            await _rocksDbClient.Connect();

            await _rocksDbClient.Use(namespace_, database);
            _logger.LogInformation("Successfully connected to SurrealDB");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to SurrealDB");
            return false;
        }
    }

    /// <summary>
    /// Disconnect from SurrealDB
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation</returns>
    public async Task DisconnectAsync()
    {
        try
        {
            if (_rocksDbClient != null)
            {
                _logger.LogInformation("Disconnecting from SurrealDB");
                await _rocksDbClient.DisposeAsync();
                _logger.LogInformation("Successfully disconnected from SurrealDB");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during SurrealDB disconnection");
        }
    }

    public async Task<DbResult<T>> CreateAsync<T>(string table, T data) where T : class
    {
        try
        {
            _logger.LogDebug("Creating record in table: {Table}", table);
            var result = await _rocksDbClient.Create<T>(table, data);
            _logger.LogDebug("Successfully created record in table: {Table}", table);
            return DbResult<T>.Success(result);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to create record in table: {Table}", table);
            return DbResult<T>.Failure(exception.Message);
        }
    }

    /// <summary>
    /// Get a record from a table by id
    /// </summary>
    /// <typeparam name="T">The type of the record to get</typeparam>
    /// <param name="table">The table to get the record from</param>
    /// <param name="id">The id of the record to get</param>
    /// <returns>A <see cref="DbResult{T}"/> containing the record if successful, otherwise an error message</returns>
    public async Task<DbResult<T>> GetAsync<T>(string table, string id) where T : class
    {
        try
        {
            _logger.LogDebug("Getting record from table: {Table}, id: {Id}", table, id);
            var result = await _rocksDbClient.Select<T>($"{table}:{id}");
            return DbResult<T>.Success(result.FirstOrDefault() ?? default!);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to get record from table: {Table}, id: {Id}", table, id);
            return DbResult<T>.Failure(exception.Message);
        }
    }

    /// <summary>
    /// Get all records from a table
    /// </summary>
    /// <typeparam name="T">The type of the records to get</typeparam>
    /// <param name="table">The table to get the records from</param>
    /// <returns>A <see cref="DbResult{IEnumerable{T}}}"/> containing the records if successful, otherwise an error message</returns>
    public async Task<DbResult<IEnumerable<T>>> GetAllAsync<T>(string table) where T : class
    {
        try
        {
            _logger.LogDebug("Getting all records from table: {Table}", table);
            var result = await _rocksDbClient.Select<T>(table);
            _logger.LogDebug("Retrieved {Count} records from table: {Table}", result?.Count() ?? 0, table);
            return DbResult<IEnumerable<T>>.Success(result ?? Enumerable.Empty<T>());
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to get all records from table: {Table}", table);
            return DbResult<IEnumerable<T>>.Failure(exception.Message);
        }
    }

    /// <summary>
    /// Update a record in a table by id
    /// </summary>
    /// <typeparam name="T">The type of the record to update</typeparam>
    /// <param name="table">The table to update the record in</param>
    /// <param name="id">The id of the record to update</param>
    /// <param name="data">The data to update the record with</param>
    /// <returns>A <see cref="DbResult{T}"/> containing the updated record if successful, otherwise an error message</returns>
    public async Task<DbResult<T>> UpdateAsync<T>(string table, string id, T data) where T : class
    {
        try
        {
            _logger.LogDebug("Updating record in table: {Table}, id: {Id}", table, id);
            var result = await _rocksDbClient.Update<T>($"{table}:{id}", data);
            _logger.LogDebug("Successfully updated record in table: {Table}, id: {Id}", table, id);
            return DbResult<T>.Success(result.FirstOrDefault() ?? default!);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to update record in table: {Table}, id: {Id}", table, id);
            return DbResult<T>.Failure(exception.Message);
        }
    }

    public async Task<DbResult<bool>> DeleteAsync(string table, string id)
    {
        try
        {
            _logger.LogDebug("Deleting record from table: {Table}, id: {Id}", table, id);
            await _rocksDbClient.Delete($"{table}:{id}");
            _logger.LogDebug("Successfully deleted record from table: {Table}, id: {Id}", table, id);
            return DbResult<bool>.Success(true);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to delete record from table: {Table}, id: {Id}", table, id);
            return DbResult<bool>.Failure(exception.Message);
        }
    }

    public async Task<DbResult<IEnumerable<T>>> QueryAsync<T>(string table, string query, Dictionary<string, object>? vars = null, CancellationToken cancellationToken = default) where T : class
    {
        try
        {
            _logger.LogDebug("Executing query on table: {Table}, query: {Query}", table, query);

            var handler = new QueryInterpolatedStringHandler();
            handler.AppendLiteral(table);
            handler.AppendLiteral(":");
            handler.AppendLiteral(query);

            var result = await _rocksDbClient.Query(handler, cancellationToken);
            var t = result.GetValue<T>(0);
            _logger.LogDebug("Query executed successfully on table: {Table}", table);
            return DbResult<IEnumerable<T>>.Success(t != null ? [t] : []);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to execute query on table: {Table}, query: {Query}", table, query);
            return DbResult<IEnumerable<T>>.Failure(exception.Message);
        }
    }

    public async Task<bool> BeginTransactionAsync()
    {
        try
        {
            _logger.LogDebug("Beginning transaction");
            await _rocksDbClient.Query($"BEGIN TRANSACTION");
            _logger.LogDebug("Transaction began successfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to begin transaction");
            return false;
        }
    }

    public async Task<bool> CommitTransactionAsync()
    {
        try
        {
            _logger.LogDebug("Committing transaction");
            await _rocksDbClient.Query($"COMMIT TRANSACTION");
            _logger.LogDebug("Transaction committed successfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to commit transaction");
            return false;
        }
    }

    public async Task<bool> RollbackTransactionAsync()
    {
        try
        {
            _logger.LogDebug("Rolling back transaction");
            await _rocksDbClient.Query($"CANCEL TRANSACTION");
            _logger.LogDebug("Transaction rolled back successfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to rollback transaction");
            return false;
        }
    }
}