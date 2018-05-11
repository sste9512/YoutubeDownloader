using System;
using System.Windows;

namespace YoutubeDownloader
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            /* if (Config.Instance.AppPath == null)
             {
                 Config.Instance.DefaultProjectPath = System.AppDomain.CurrentDomain.BaseDirectory.Replace(@"\", @"/") + "config.txt";
                 Config.Instance.RetrieveAllSettingsFromConfig();
             }

     */
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            // MessageBox.Show("This is the activated lifecycle");
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);

            //Config.Instance.SaveAllToConfigFile();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            //  Config.Instance.SaveAllToConfigFile();
        }
    }
}