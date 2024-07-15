using System.IO;
using System.Runtime.CompilerServices;
using MediatR;

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.CQRS.Pipelines;

public sealed class GenericStreamPipelineBehavior<TRequest, TResponse> : IStreamPipelineBehavior<TRequest, TResponse>
{
    private readonly TextWriter _writer;

    public GenericStreamPipelineBehavior(TextWriter writer)
    {
        _writer = writer;
    }

    public async IAsyncEnumerable<TResponse> Handle(TRequest request, StreamHandlerDelegate<TResponse> next, [EnumeratorCancellation]CancellationToken cancellationToken)
    {
        await _writer.WriteLineAsync("-- Handling StreamRequest");
        await foreach (var response in next().WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            yield return response;
        }
        await _writer.WriteLineAsync("-- Finished StreamRequest");
    }
}