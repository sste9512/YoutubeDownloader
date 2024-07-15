using LiteDB;
using OneOf;
using System.IO;

namespace YoutubeDownloader_WPFCore.Infrastructure.Stores
{
    public class ImageStore
    {
        private readonly LiteDatabase _liteDatabase;

        public ImageStore(LiteDatabase liteDatabase)
        {
            _liteDatabase = liteDatabase;
        }

        public LiteFileInfo<string> SaveImageFromFileOrStream(OneOf<string, Stream> fileFormat)
        {
            var liteStorage = _liteDatabase.GetStorage<string>("myFiles", "myChunks");

            return fileFormat.IsT0 ?
                // Upload a file from file system
                liteStorage.Upload("$/photos/2014/picture-01.jpg", @"C:\Temp\picture-01.jpg") :
                // Upload a file from a Stream
                liteStorage.Upload("$/photos/2014/picture-01.jpg", "picture-01.jpg", fileFormat.AsT1);
        }

        /*public QueryAsStream(string fieldId, Stream inputStream)
        {
            var liteStorage = _liteDatabase.GetStorage<string>("myFiles", "myChunks");

            // Find file reference only - returns null if not found
            var file = liteStorage.FindById(fieldId);

            // Or get binary data as Stream and copy to another Stream
            file.CopyTo(inputStream);
        }*/

        /*public void QueryImage(string fileId, )
        {

            var liteStorage = _liteDatabase.GetStorage<string>("myFiles", "myChunks");

            // Find file reference only - returns null if not found
            var file = liteStorage.FindById("$/photos/2014/picture-01.jpg");

            // Now, load binary data and save to file system
             file.SaveAs(@"C:\Temp\new-picture.jpg");

            // Or get binary data as Stream and copy to another Stream
            file.CopyTo(Response.OutputStream);

            // Find all files references in a "directory"
            var files = liteStorage.Find("$/photos/2014/");
        }*/
    }
}
