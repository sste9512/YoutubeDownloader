using MediaToolkit;
using MediaToolkit.Model;
using YoutubeExplode.Models;

namespace YoutubeDownloader.Domain.Conversion
{
    public class AudioConversion
    {

        public AudioConversion ConvertAndWriteToFile()
        {
            var inputFile = new MediaFile {Filename = @"C:\Path\To_Video.flv"};
            var outputFile = new MediaFile {Filename = @"C:\Path\To_Save_New_Video.mp4"};

            using (var engine = new Engine())
            {
                engine.Convert(inputFile, outputFile);
                return this;
            }
        }
    }
}