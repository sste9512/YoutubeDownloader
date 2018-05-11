using System.IO;

namespace YoutubeDownloader.Config
{
    internal class Config
    {
        public string SavedURLSPath
        {
            get; set;
        }

        public string PlayListsPath
        {
            get; set;
        }

        public string AppPath
        {
            get; set;
        }

        public bool needsPathSetup
        {
            get; set;
        }

        private static Config instance = new Config();

        private Config()
        {
        }

        public static Config Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Config();
                }
                return instance;
            }
        }

        public void SaveAllToConfigFile()
        {
            using (StreamWriter sw = new StreamWriter(AppPath))
            {
                /*  sw.WriteLine("KotorPath = " + KotorPath);
                  sw.WriteLine("KotorPath = " + Kotor2Path);
                  sw.WriteLine("DefaultProjectPath = " + AppPath);
                  sw.Close();*/
            }
        }

        public void RetrieveAllSettingsFromConfig()
        {
            /*  if (File.Exists(AppPath))
              {
                  string[] config = File.ReadAllLines(DefaultProjectPath);

                  KotorPath = config[0].Split('=')[1].Trim();
                  Kotor2Path = config[1].Split('=')[1].Trim();
                  DefaultProjectPath = config[2].Split('=')[1].Trim();
              }*/
        }
    }
}