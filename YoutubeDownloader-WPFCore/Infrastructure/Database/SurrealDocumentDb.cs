using SurrealDb.Embedded.RocksDb;
using SurrealDb.Net;
using SurrealDb.Net.Handlers;
using YoutubeDownloader_WPFCore.Application.Interfaces;
using YoutubeDownloader_WPFCore.Infrastructure.Values;

namespace YoutubeDownloader_WPFCore.Infrastructure.Database;

public sealed class SurrealDocumentDb(SurrealDbRocksDbClient rocksDbClient) : IDocumentDb
{
    public async Task<bool> ConnectAsync(string url, string username, string password, string namespace_, string database)
    {
        try
        {
            await rocksDbClient.Connect();
       
            await rocksDbClient.Use(namespace_, database);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task DisconnectAsync()
    {
        if (rocksDbClient != null)
        {
            await rocksDbClient.DisposeAsync();
        }
    }

    public async Task<DbResult<T>> CreateAsync<T>(string table, T data) where T : class
    {
        try
        {
            var result = await rocksDbClient.Create<T>(table, data);
            return DbResult<T>.Success(result);
        }
        catch(Exception exception)
        {
            return DbResult<T>.Failure(exception.Message);
        }
    }

    public async Task<DbResult<T>> GetAsync<T>(string table, string id) where T : class
    {
        try
        {
            var result = await rocksDbClient.Select<T>($"{table}:{id}");
            return DbResult<T>.Success(result.FirstOrDefault());
        }
        catch(Exception exception)
        {
            return DbResult<T>.Failure(exception.Message);
        }
    }

    public async Task<DbResult<IEnumerable<T>>> GetAllAsync<T>(string table) where T : class
    {
        try
        {
            var result = await rocksDbClient.Select<T>(table);
            return DbResult<IEnumerable<T>>.Success(result);
        }
        catch(Exception exception)
        {
            return DbResult<IEnumerable<T>>.Failure(null);
        }
    }

    public async Task<DbResult<T>> UpdateAsync<T>(string table, string id, T data) where T : class
    {
        try
        {
            var result = await rocksDbClient.Update<T>($"{table}:{id}", data);
            return DbResult<T>.Success(result.FirstOrDefault());
        }
        catch(Exception exception)
        {
            return DbResult<T>.Failure(null);
        }
    }

    public async Task<DbResult<bool>> DeleteAsync(string table, string id)
    {
        try
        {
            await rocksDbClient.Delete($"{table}:{id}");
            return DbResult<bool>.Success(true);
        }
        catch(Exception exception)
        {
            return DbResult<bool>.Failure(exception.Message);
        }
    }

    public async Task<DbResult<IEnumerable<T>>> QueryAsync<T>(string table, string query, Dictionary<string, object>? vars = null, CancellationToken cancellationToken = default) where T : class
    {
        try
        {
            
            var handler = new QueryInterpolatedStringHandler();
            handler.AppendLiteral(table);
            handler.AppendLiteral(":");
            handler.AppendLiteral(query);
         
            var result = await rocksDbClient.Query(handler, cancellationToken);
            var t =  result.GetValue<T>(0);
            return DbResult<IEnumerable<T>>.Success([t]!);
        }
        catch(Exception exception)
        {
            return DbResult<IEnumerable<T>>.Failure(exception.Message);
        }
    }

    public async Task<bool> BeginTransactionAsync()
    {
        try
        {
            await rocksDbClient.Query($"BEGIN TRANSACTION");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> CommitTransactionAsync()
    {
        try
        {
            await rocksDbClient.Query($"COMMIT TRANSACTION");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RollbackTransactionAsync()
    {
        try
        {
            await rocksDbClient.Query($"CANCEL TRANSACTION");
            return true;
        }
        catch
        {
            return false;
        }
    }
}