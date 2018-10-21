

using System;
using System.Windows;
using YoutubeDownloader.Config;

namespace YoutubeDownloader
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (YtConfig.GetInstance.AppPath != null) return;
            
            YtConfig.GetInstance.AppPath =
                System.AppDomain.CurrentDomain.BaseDirectory.Replace(@"\", @"/") + "config.txt";
            YtConfig.GetInstance.RetrieveAllSettingsFromConfig();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            MessageBox.Show("This is the activated lifecycle");
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);
            YtConfig.GetInstance.SaveAllToConfigFile();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            YtConfig.GetInstance.SaveAllToConfigFile();
        }
    }
}