using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ENTech.Store.Api.BL
{
    public abstract class FileStorageBase
    {
        public abstract Task<string> Save(string fileName, Stream s, Action<int> updateStatus);
    }

    public class LocalFileStorage : FileStorageBase
    {
        public override async Task<string> Save(string fileName, Stream s, Action<int> updateStatus)
        {
            var path = @"M:\_tempo\Files\" + fileName;
            var b = new byte[1024 * 256];//256kb

            using (var fs = new FileStream(path, FileMode.CreateNew))
            {
                while (s.Position < s.Length)
                {
                    //write to file / IFileWriter
                    var actualLen = await s.ReadAsync(b, 0, b.Length);
                    await fs.WriteAsync(b, 0, actualLen);

                    updateStatus((int)s.Position);
                    await Task.Delay(1000);
                }

                await fs.FlushAsync();
            }

            //save to cdn
            return "cdn:url";
        }
    }

    //public class AzureFileStorage : FileStorageBase
    //{
    //    public override async Task<string> Save(string fileName, Stream s, Action<int> updateStatus)
    //    {
    //        var path = @"M:\_tempo\Files\" + fileName;
    //        var b = new byte[1024 * 256]; //256kb

    //        using (var fs = new FileStream(path, FileMode.CreateNew))
    //        {
    //            while (s.Position < s.Length)
    //            {
    //                //write to file / IFileWriter
    //                var actualLen = await s.ReadAsync(b, 0, b.Length);
    //                await fs.WriteAsync(b, 0, actualLen);

    //                updateStatus((int)s.Position);
    //            }

    //            await fs.FlushAsync();
    //        }

    //        //save to cdn
    //        return "cdn:url";
    //    }
    //}
}