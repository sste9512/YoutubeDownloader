﻿using MediaToolkit;
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
        
        private string _outputFile = "";
        private string _inputFile = "";
        private int _videoType = 0;
        private Video _video;
       
        
        public  AudioConversion SetOutPutFile(string output)
        {
            _outputFile = output;
            return this;
        }

        public AudioConversion SetInputFile(string input)
        {
            _inputFile = input;
            return this;
        }

        public AudioConversion SetVideoType(int vidType)
        {
            _videoType = vidType;
            return this;
        }

        public AudioConversion SetVideo(Video video)
        {
            this._video = video;
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
