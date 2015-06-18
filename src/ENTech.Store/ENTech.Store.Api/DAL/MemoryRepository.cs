using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENTech.Store.Api.DAL
{
    public class MemoryRepository<T>
    {
        private readonly Dictionary<string, T> _storage = new Dictionary<string, T>();

        public void Create(string id, T data)
        {
            _storage.Add(id, data);
        }

        public void Update(string id, T data)
        {
            _storage[id] = data;
        }

        public T GetById(string id)
        {
            return _storage[id];
        }
    }




    public class Product
    {
        public string Id;
        public string Title;
        public string Description;
        public string LogoUploadId;
        public string LogoUrl;
    }



    public class ProductRepository : MemoryRepository<Product>
    {
    }




    public class FileUpload
    {
        public string Id;
        public string OwnerId;
        public int Size;
        public string FileName;

        public int Uploaded;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
        public string AttachedEntityType;
        public string AttachedEntityId;

        public bool IsProcessed;
        public bool IsAttached;
        public bool IsUploaded;
    }

    public class UploadRepository : MemoryRepository<FileUpload>
    {
    }

}