using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YoutubeDownloader_WPFCore.Infrastructure.Values;

namespace YoutubeDownloader_WPFCore.Application.Interfaces;

public interface IDocumentDb
{
    Task<bool> ConnectAsync(string url, string username, string password, string namespace_, string database);
    Task DisconnectAsync();

    Task<DbResult<T>> CreateAsync<T>(string table, T data) where T : class;
    Task<DbResult<T>> GetAsync<T>(string table, string id) where T : class;
    Task<DbResult<IEnumerable<T>>> GetAllAsync<T>(string table) where T : class;
    Task<DbResult<T>> UpdateAsync<T>(string table, string id, T data) where T : class;
    Task<DbResult<bool>> DeleteAsync(string table, string id);

    Task<DbResult<IEnumerable<T>>> QueryAsync<T>(string table, string query, Dictionary<string, object>? vars = null, CancellationToken cancellationToken = default) where T : class;
    Task<bool> BeginTransactionAsync();
    Task<bool> CommitTransactionAsync();
    Task<bool> RollbackTransactionAsync();
}