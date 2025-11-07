using System.Runtime.CompilerServices;

namespace YoutubeDownloader_WPFCore.Infrastructure.Values;




public sealed class DbResult<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? ErrorMessage { get; }
    public DbError ErrorType { get; }

    private DbResult(bool isSuccess, T? value, string? errorMessage, DbError errorType)
    {
        IsSuccess = isSuccess;
        Value = value;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }

    public static DbResult<T> Success(T value) => new(true, value, null, DbError.None);

    public static DbResult<T> Failure(string message, DbError error = DbError.Unknown) => 
        new(false, default, message, error);


    public void Match(Action<T> onSuccess, Action<string> onFailure)
    {
        if (IsSuccess)
        {
            onSuccess(Value!);
        }
        else
        {
            onFailure(ErrorMessage!);
        }
    }
}

public enum DbError
{
    None,
    ConnectionFailed,
    QueryFailed, 
    TransactionFailed,
    DataNotFound,
    DuplicateKey,
    InvalidData,
    Unknown
}