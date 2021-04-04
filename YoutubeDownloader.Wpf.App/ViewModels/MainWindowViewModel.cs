using System.Collections.Generic;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeDownloader.ViewModels
{
    public class MainWindowViewModel
    {
        public bool IsBusy;
        public string Query;
        public YoutubeClient Client = new YoutubeClient();
        public Channel Channel;
        public MediaStreamInfoSet MediaStreamInfos;
        public MediaStreamInfo MediaStream;
        public IReadOnlyList<ClosedCaptionTrackInfo> ClosedCaptionTrackInfos;
        public double Progress;
        public bool IsProgressIndeterminate;
        public Video Video;

        public MainWindowViewModel()
        {
        }
    }
}