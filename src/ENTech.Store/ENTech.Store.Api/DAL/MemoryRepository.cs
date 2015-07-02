using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENTech.Store.Api.DAL
{
    public static class MemoryRepository
    {
        public static Dictionary<string, Dictionary<string, object>> Storage = new Dictionary<string, Dictionary<string, object>>();
        
        public static void Clear()
        {
            Storage.Clear();
        }
    }

    public class MemoryRepository<T>
    {
       
      // private Dictionary<string, object> _storage =  
        public Dictionary<string, object> _table {
            get
            {
                var s = MemoryRepository.Storage;
                var key = typeof (T).ToString();
                if (!s.ContainsKey(key)) 
                    s.Add(key, new Dictionary<string, object>());
                
                return s[key];
            }
        }

        public void Create(string id, T data)
        {
            _table.Add(id, data);
        }

       

        public void Update(string id, T data)
        {
            _table[id] = data;
        }

        public void Delete(string id)
        {
            _table.Remove(id);
        }

        public bool Exists(string id)
        {
            return _table.ContainsKey(id);
        }

        public T GetById(string id)
        {
            return (T)_table[id];
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
        public string FileExtension;

        public long Uploaded;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
        public string AttachedEntityType;
        public string AttachedEntityId;
        public string AttachedEntityFieldName;

        public bool IsProcessed;
        public bool IsAttached;
        public bool IsUploaded;
        public string URL;

    }

    public class UploadRepository : MemoryRepository<FileUpload>
    {
    }

}