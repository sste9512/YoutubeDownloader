

namespace YoutubeDownloader_WPFCore.Infrastructure
{
    public class Deferrable : IDisposable
    {
        private Action Action { get; set; }

     
        public void Dispose()
        {
              Action.Invoke();
        }
    }
}
