using MediaToolkit;
using MediaToolkit.Model;
using YoutubeExplode.Videos;


namespace YoutubeDownloader.Conversion
{
    public class AudioConversion
    {
        private string _outputFile = "";
        private string _inputFile = "";
        private int _videoType = 0;
        private Video _video;



        public AudioConversion ConvertAndWriteToFile()
        {
            var inputFile = new MediaFile {Filename = @"C:\Path\To_Video.flv"};
            var outputFile = new MediaFile {Filename = @"C:\Path\To_Save_New_Video.mp4"};

            using var engine = new Engine();
            
            engine.Convert(inputFile, outputFile);

            return this;
        }
    }
}