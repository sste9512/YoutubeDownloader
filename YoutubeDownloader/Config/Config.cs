using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace YoutubeDownloader.Config
{
    public class YtConfig
    {
        public string SavedUrlsPath { get; set; }

        public string PlayListsPath { get; set; }

        public string AppPath
        {
            get => AppDomain.CurrentDomain.BaseDirectory;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Value cannot be null or empty.", nameof(value));
            }
        }
       
        public bool NeedsPathSetup { get; set; }

        private static YtConfig _instance = new YtConfig();

        private YtConfig()
        {
        }

        public static YtConfig GetInstance => _instance ?? (_instance = new YtConfig());

        public void SaveAllToConfigFile()
        {
            Task.Factory.StartNew(() =>
            {
                using (var sw = new StreamWriter(AppPath))
                {
                    sw.WriteLine("YoutubeDownloaderPath = " + AppDomain.CurrentDomain.BaseDirectory);
                    sw.Close();
                }
            });
        }

       
        public void RetrieveAllSettingsFromConfig()
        {
            Task.Factory.StartNew(() =>
            {
                if (!File.Exists(AppPath)) return;
                var config = File.ReadAllLines(AppPath);
                //ADD FUNCTIONS FOR READING CONFIG FILE
            });
        }
    }
}