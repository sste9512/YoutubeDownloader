using MediatR;
using Microsoft.Extensions.Logging;

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Pipelines;

public sealed class LoggingBehaviour<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    
    private readonly ILogger _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = "Main User";
        string? userName = string.Empty;

        /*if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }*/

        _logger.LogInformation("Regflow Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);

        return await next();
    }
}