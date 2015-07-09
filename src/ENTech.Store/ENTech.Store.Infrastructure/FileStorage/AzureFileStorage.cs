using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.FileStorage
{
    public class AzureFileStorage : FileStorageBase
    {
        public override Task<string> Save(string fileName, System.IO.Stream s, Action<int> updateStatus)
        {
            throw new NotImplementedException();
        }
    }
}
