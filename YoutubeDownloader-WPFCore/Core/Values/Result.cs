namespace YoutubeDownloader_WPFCore.Core.Values;


/// <summary>
/// Represents a result that is either Ok and contains a value, or Err and contains an error.
/// This is similar to Rust's Result type.
/// </summary>
/// <typeparam name="T">The success value type</typeparam>
/// <typeparam name="E">The error value type</typeparam>
public sealed class Result<T, E>
{
    private readonly bool _isOk;
    private readonly T? _value;
    private readonly E? _error;

    private Result(bool isOk, T? value, E? error)
    {
        _isOk = isOk;
        _value = value;
        _error = error;
    }

    /// <summary>
    /// Creates an Ok result containing the given value.
    /// </summary>
    public static Result<T, E> Ok(T value) => new(true, value, default);

    /// <summary>
    /// Creates an Err result containing the given error.
    /// </summary>
    public static Result<T, E> Err(E error) => new(false, default, error);

    /// <summary>
    /// Returns true if the result is Ok.
    /// </summary>
    public bool IsOk => _isOk;

    /// <summary>
    /// Returns true if the result is Err.
    /// </summary>
    public bool IsErr => !_isOk;

    /// <summary>
    /// Returns the contained Ok value, or throws an exception if the result is an Err.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the Result is Err</exception>
    public T Unwrap()
    {
        if (IsErr)
            throw new InvalidOperationException($"Called Unwrap on an Err value: {_error}");
        return _value!;
    }

    /// <summary>
    /// Returns the contained Err value, or throws an exception if the result is Ok.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the Result is Ok</exception>
    public E UnwrapErr()
    {
        if (IsOk)
            throw new InvalidOperationException("Called UnwrapErr on an Ok value");
        return _error!;
    }

    /// <summary>
    /// Returns the contained Ok value or the provided default value.
    /// </summary>
    public T UnwrapOr(T defaultValue) =>
        IsOk ? _value! : defaultValue;

    /// <summary>
    /// Returns the contained Ok value or computes it from the Err value.
    /// </summary>
    public T UnwrapOrElse(Func<E, T> op) =>
        IsOk ? _value! : op(_error!);

    /// <summary>
    /// Maps a Result<T, E> to Result<U, E> by applying a function to the contained Ok value.
    /// </summary>
    public Result<U, E> Map<U>(Func<T, U> mapper) =>
        IsOk ? Result<U, E>.Ok(mapper(_value!)) : Result<U, E>.Err(_error!);

    /// <summary>
    /// Maps a Result<T, E> to Result<T, F> by applying a function to the contained Err value.
    /// </summary>
    public Result<T, F> MapErr<F>(Func<E, F> mapper) =>
        IsOk ? Result<T, F>.Ok(_value!) : Result<T, F>.Err(mapper(_error!));

    /// <summary>
    /// Returns the provided default (if Err), or applies a function to the contained value (if Ok).
    /// </summary>
    public U MapOr<U>(U defaultValue, Func<T, U> mapper) =>
        IsOk ? mapper(_value!) : defaultValue;

    /// <summary>
    /// Maps a Result<T, E> to U by applying a function to the contained Ok value, 
    /// or a fallback function to the contained Err value.
    /// </summary>
    public U MapOrElse<U>(Func<E, U> defaultOp, Func<T, U> mapper) =>
        IsOk ? mapper(_value!) : defaultOp(_error!);

    /// <summary>
    /// Returns the result of applying a function to the contained value (if Ok), 
    /// or returns the Err value (if Err).
    /// </summary>
    public Result<U, E> AndThen<U>(Func<T, Result<U, E>> op) =>
        IsOk ? op(_value!) : Result<U, E>.Err(_error!);

    /// <summary>
    /// Returns the result if it's Ok, otherwise returns the provided result.
    /// </summary>
    public Result<T, E> Or(Result<T, E> res) =>
        IsOk ? this : res;

    /// <summary>
    /// Returns the result if it's Ok, otherwise calls the provided function and returns its result.
    /// </summary>
    public Result<T, E> OrElse(Func<E, Result<T, E>> op) =>
        IsOk ? this : op(_error!);

    /// <summary>
    /// Returns Ok if either self or res is Ok, otherwise returns the Err value of res.
    /// </summary>
    public Result<T, E> And(Result<T, E> res) =>
        IsOk ? res : this;

    /// <summary>
    /// Executes the provided action if the result is Ok, otherwise does nothing.
    /// </summary>
    public void IfOk(Action<T> action)
    {
        if (IsOk)
            action(_value!);
    }

    /// <summary>
    /// Executes the provided action if the result is Err, otherwise does nothing.
    /// </summary>
    public void IfErr(Action<E> action)
    {
        if (IsErr)
            action(_error!);
    }

    /// <summary>
    /// Pattern matches on this result and executes either okFunc or errFunc based on the variant.
    /// </summary>
    public U Match<U>(Func<T, U> okFunc, Func<E, U> errFunc) =>
        IsOk ? okFunc(_value!) : errFunc(_error!);

    /// <summary>
    /// Pattern matches on this result and executes either okAction or errAction based on the variant.
    /// </summary>
    public void Match(Action<T> okAction, Action<E> errAction)
    {
        if (IsOk)
            okAction(_value!);
        else
            errAction(_error!);
    }

    /// <summary>
    /// Converts the Result to a string.
    /// </summary>
    public override string ToString() =>
        IsOk ? $"Ok({_value})" : $"Err({_error})";

    /// <summary>
    /// Returns true if this Result equals the other Result.
    /// </summary>
    public override bool Equals(object? obj) =>
        obj is Result<T, E> other &&
        IsOk == other.IsOk &&
        (IsOk
            ? EqualityComparer<T>.Default.Equals(_value, other._value)
            : EqualityComparer<E>.Default.Equals(_error, other._error));

    /// <summary>
    /// Returns a hash code for this Result.
    /// </summary>
    public override int GetHashCode() =>
        IsOk ? HashCode.Combine(true, _value) : HashCode.Combine(false, _error);

    /// <summary>
    /// Implicit conversion from T to Result<T, E> (Ok).
    /// </summary>
    public static implicit operator Result<T, E>(T value) => Ok(value);

    /// <summary>
    /// Implicit conversion from E to Result<T, E> (Err).
    /// </summary>
    public static implicit operator Result<T, E>(E error) => Err(error);

    /// <summary>
    /// Explicit conversion from Result<T, E> to T. Throws if the result is Err.
    /// </summary>
    public static explicit operator T(Result<T, E> result) => result.Unwrap();

    /// <summary>
    /// Explicit conversion from Result<T, E> to E. Throws if the result is Ok.
    /// </summary>
    public static explicit operator E(Result<T, E> result) => result.UnwrapErr();
}

/// <summary>
/// Static methods for working with Result values.
/// </summary>
public static class Result
{
    /// <summary>
    /// Creates an Ok result containing the given value.
    /// </summary>
    public static Result<T, E> Ok<T, E>(T value) => Result<T, E>.Ok(value);

    /// <summary>
    /// Creates an Err result containing the given error.
    /// </summary>
    public static Result<T, E> Err<T, E>(E error) => Result<T, E>.Err(error);

    /// <summary>
    /// Wrap a function call in a try-catch block and returns a Result.
    /// </summary>
    public static Result<T, Exception> Try<T>(Func<T> func)
    {
        try
        {
            return Ok<T, Exception>(func());
        }
        catch (Exception ex)
        {
            return Err<T, Exception>(ex);
        }
    }

    /// <summary>
    /// Wraps an async function call in a try-catch block and returns a Result.
    /// </summary>
    public static async Task<Result<T, Exception>> TryAsync<T>(Func<Task<T>> func)
    {
        try
        {
            return Ok<T, Exception>(await func());
        }
        catch (Exception ex)
        {
            return Err<T, Exception>(ex);
        }
    }
}