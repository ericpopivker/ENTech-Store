using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.FileStorage
{
    public abstract class FileStorageBase
    {
        public abstract Task<string> Save(string fileName, Stream s, Action<int> updateStatus);
    }
}
