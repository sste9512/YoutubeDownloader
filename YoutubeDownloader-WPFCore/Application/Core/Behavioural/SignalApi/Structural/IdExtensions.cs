using YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural.Identification;

namespace YoutubeDownloader_WPFCore.Application.Core.Behavioural.SignalApi.Structural;

public static class IdExtensions
{
    public static IdRef<T> Id<T>(this string id)
    {
        return new IdRef<T>()
        {
            Id = id
        };
    }
}