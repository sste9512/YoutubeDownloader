using System.Collections.Generic;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;

namespace YoutubeDownloader.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly YoutubeClient _youtubeClient;
        public MediaStreamInfoSet MediaStreamInfos;
        public Video Video;

        public MainWindowViewModel(YoutubeClient youtubeClient)
        {
            _youtubeClient = youtubeClient;
        }
        
        
    }
}