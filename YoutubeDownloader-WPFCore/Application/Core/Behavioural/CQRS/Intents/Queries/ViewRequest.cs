using MediatR;
using OneOf.Types;

// ReSharper disable ConditionalTernaryEqualBranch

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Intents.Queries;

public abstract class ViewRequestContainer<T> where T : class
{
    public WeakReference<T> WeakView { get; set; }
    public T View => WeakView.TryGetTarget(out T target) ? target : target;

    public IRequest<T> Request { get; set; }
}

public abstract class ViewRequest<T> : IRequest<Result<T>> where T : class
{
    protected Guid Guid { get; init; } = Guid.NewGuid();
    private WeakReference<T>? ViewReference { get; set; }
    public T View => ViewReference.TryGetTarget(out T target) ? target : target;

    protected ViewRequest<T> Accept(T view)
    {
        ViewReference = new WeakReference<T>(view);
        return this;
    }

    public bool Visit(out T visitor)
    {
        if (ViewReference!.TryGetTarget(out T target))
        {
            visitor = target;
            return true;
        }

        visitor = null;
        return false;
    }
}