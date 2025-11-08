using SurrealDb.Embedded.RocksDb;
using SurrealDb.Net.Handlers;

namespace YoutubeDownloader_WPFCore.Infrastructure.Database;

public static class SurrealDbRocksDbClientExtensions
{
    public static void TryOp(this SurrealDbRocksDbClient client, Action action)
    {
        // try
        // {
        //       await 
        // }
    }


    /// <summary>
    /// Creates a relation between two records in the database
    /// </summary>
    /// <typeparam name="TIn">The type of the source record</typeparam>
    /// <typeparam name="TOut">The type of the target record</typeparam>
    /// <typeparam name="TData">The type of the relation data</typeparam>
    /// <param name="client">The SurrealDB client</param>
    /// <param name="table">The relation table name</param>
    /// <param name="from">The source record identifier</param>
    /// <param name="to">The target record identifier</param>
    /// <param name="data">The relation data</param>
    /// <returns>The created relation record</returns>
    public static async Task<TData> CreateRelationAsync<TIn, TOut, TData>(
        this SurrealDbRocksDbClient client,
        string table,
        string from,
        string to,
        TData data)
        where TData : class
    {
        var relationId = $"{table}:[{from}, {to}]";
        return await client.Create(relationId, data);
    }

    /// <summary>
    /// Gets all outgoing relations from a record
    /// </summary>
    /// <typeparam name="T">The type of the relation data</typeparam>
    /// <param name="client">The SurrealDB client</param>
    /// <param name="recordId">The source record identifier</param>
    /// <param name="relationTable">The relation table name (optional)</param>
    /// <returns>Collection of outgoing relations</returns>
    public static async Task<IEnumerable<T>> GetOutgoingRelationsAsync<T>(
        this SurrealDbRocksDbClient client,
        string recordId,
        string? relationTable = null)
        where T : class
    {
        var query = relationTable != null 
            ? $"SELECT * FROM {relationTable} WHERE in = {recordId}"
            : $"SELECT * FROM ->* WHERE in = {recordId}";
        
        
        var result = await client.Query(query);
        return result.GetValue<IEnumerable<T>>(0) ?? Enumerable.Empty<T>();
    }

    /// <summary>
    /// Gets all incoming relations to a record
    /// </summary>
    /// <typeparam name="T">The type of the relation data</typeparam>
    /// <param name="client">The SurrealDB client</param>
    /// <param name="recordId">The target record identifier</param>
    /// <param name="relationTable">The relation table name (optional)</param>
    /// <returns>Collection of incoming relations</returns>
    public static async Task<IEnumerable<T>> GetIncomingRelationsAsync<T>(
        this SurrealDbRocksDbClient client,
        string recordId,
        string? relationTable = null)
        where T : class
    {
        var handler = relationTable != null 
            ? new QueryInterpolatedStringHandler()
                .AppendLiteral("SELECT * FROM ")
                .AppendLiteral(relationTable)
                .AppendLiteral(" WHERE out = ")
                .AppendLiteral(recordId)
            : new QueryInterpolatedStringHandler()
                .AppendLiteral("SELECT * FROM <-* WHERE out = ")
                .AppendLiteral(recordId);
        
        var result = await client.Query(query);
        return result.GetValue<IEnumerable<T>>(0) ?? Enumerable.Empty<T>();
    }

    /// <summary>
    /// Gets all records connected to a record through outgoing relations
    /// </summary>
    /// <typeparam name="T">The type of the connected records</typeparam>
    /// <param name="client">The SurrealDB client</param>
    /// <param name="recordId">The source record identifier</param>
    /// <param name="relationTable">The relation table name (optional)</param>
    /// <returns>Collection of connected records</returns>
    public static async Task<IEnumerable<T>> GetConnectedRecordsAsync<T>(
        this SurrealDbRocksDbClient client,
        string recordId,
        string? relationTable = null)
        where T : class
    {
        var query = relationTable != null 
            ? $"SELECT out.* FROM {relationTable} WHERE in = {recordId}"
            : $"SELECT ->*.out FROM {recordId}";
        
        var result = await client.Query(query);
        return result.GetValue<IEnumerable<T>>(0) ?? Enumerable.Empty<T>();
    }

    /// <summary>
    /// Gets all records that connect to a record through incoming relations
    /// </summary>
    /// <typeparam name="T">The type of the connecting records</typeparam>
    /// <param name="client">The SurrealDB client</param>
    /// <param name="recordId">The target record identifier</param>
    /// <param name="relationTable">The relation table name (optional)</param>
    /// <returns>Collection of connecting records</returns>
    public static async Task<IEnumerable<T>> GetConnectingRecordsAsync<T>(
        this SurrealDbRocksDbClient client,
        string recordId,
        string? relationTable = null)
        where T : class
    {
        var query = relationTable != null 
            ? $"SELECT in.* FROM {relationTable} WHERE out = {recordId}"
            : $"SELECT <-*.in FROM {recordId}";
        
        var result = await client.Query(query);
        return result.GetValue<IEnumerable<T>>(0) ?? Enumerable.Empty<T>();
    }

    /// <summary>
    /// Deletes a specific relation between two records
    /// </summary>
    /// <param name="client">The SurrealDB client</param>
    /// <param name="table">The relation table name</param>
    /// <param name="from">The source record identifier</param>
    /// <param name="to">The target record identifier</param>
    /// <returns>Task representing the async operation</returns>
    public static async Task DeleteRelationAsync(
        this SurrealDbRocksDbClient client,
        string table,
        string from,
        string to)
    {
        var relationId = $"{table}:[{from}, {to}]";
        await client.Delete(relationId);
    }

    /// <summary>
    /// Performs a graph traversal starting from a record
    /// </summary>
    /// <typeparam name="T">The type of the result records</typeparam>
    /// <param name="client">The SurrealDB client</param>
    /// <param name="startRecord">The starting record identifier</param>
    /// <param name="traversalPattern">The traversal pattern (e.g., "->knows->person", "<-follows<-user")</param>
    /// <param name="depth">Maximum traversal depth (optional)</param>
    /// <returns>Collection of records found through traversal</returns>
    public static async Task<IEnumerable<T>> TraverseGraphAsync<T>(
        this SurrealDbRocksDbClient client,
        string startRecord = "*",
        string traversalPattern = "->*",
        int? depth = 1)
        where T : class
    {
        var query = depth.HasValue 
            ? $"SELECT * FROM {startRecord}{traversalPattern} LIMIT {depth}"
            : $"SELECT * FROM {startRecord}{traversalPattern}";

        var handler = new QueryInterpolatedStringHandler();
        handler.AppendLiteral("SELECT * FROM ");
        handler.AppendLiteral(startRecord);
        handler.AppendLiteral(traversalPattern);
        
        if (depth.HasValue)
        {
            handler.AppendLiteral(" LIMIT ");
            handler.AppendLiteral(depth.Value.ToString());
        }

        var result = await client.Query(handler);
        return result.GetValue<IEnumerable<T>>(0) ?? [];
    }
}