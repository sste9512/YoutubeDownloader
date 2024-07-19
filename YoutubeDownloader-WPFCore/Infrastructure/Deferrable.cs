using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
