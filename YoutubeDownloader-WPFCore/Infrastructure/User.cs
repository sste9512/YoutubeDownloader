using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeDownloader_WPFCore.Application.Interfaces;

namespace YoutubeDownloader_WPFCore.Infrastructure
{
    public class User : IUser
    {
        public string Id { get; }
    }
}
