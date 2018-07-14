using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;



namespace YoutubeDownloader.Conversion
{

  
    public  class AudioConversion
    {
        
        private string outputFile = "";
        private string inputFile = "";
        private int videoType = 0;
        private Video video;
       
        
        public  AudioConversion setOutPutFile(string output)
        {
            outputFile = output;
            return this;
        }

        public AudioConversion setInputFile(string input)
        {
            inputFile = input;
            return this;
        }

        public AudioConversion setVideoType(int vidType)
        {
            videoType = vidType;
            return this;
        }

        public AudioConversion setVideo(Video video)
        {
            this.video = video;
            return this;
        }
        
        public AudioConversion ConvertAndWriteToFile()
        {



            var inputFile = new MediaFile { Filename = @"C:\Path\To_Video.flv" };
            var outputFile = new MediaFile { Filename = @"C:\Path\To_Save_New_Video.mp4" };

            using (var engine = new Engine())
            {
                engine.Convert(inputFile, outputFile);
            }











            

            return this;
        }
        
    }
}
