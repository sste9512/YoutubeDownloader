using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using YoutubeDownloader_WPFCore.Application.Interfaces;

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Pipelines;

public sealed class PerformanceBehaviour<TRequest, TResponse>(
    ILogger<TRequest> logger,
    IUser user) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    
    private readonly Stopwatch _timer = new();
    

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        //if (elapsedMilliseconds <= 500) return response;
        var requestName = typeof(TRequest).Name;
        var userId = user.Id ?? string.Empty;
        var userName = string.Empty;

        logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
            requestName, elapsedMilliseconds, userId, userName, request);

        return response;
    }
}
