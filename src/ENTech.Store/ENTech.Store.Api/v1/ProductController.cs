using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ENTech.Store.Api.DAL;

namespace ENTech.Store.Api.v1
{

    public class FileBaseDto
    {
        public string Type;

        public FileBaseDto(string type)
        {
            this.Type = type;
        }
    }

    public class UploadReferenceDto: FileBaseDto
    {
        public string Id;
        public int StatusPercentage;

        public UploadReferenceDto():base("Upload")
        {
        }
    }

    public class UrlDto : FileBaseDto
    {
        public string Url;

        public UrlDto()
            : base("Upload")
        {
        }
    }


    public class ProductDto
    {
        public string Id;
        public string Title;
        public string Description;
        public FileBaseDto Logo;
    }


    public class ProductService
    {
        private readonly ProductRepository _repository = new ProductRepository();

        public void Create(Product product)
        {
            _repository.Create(product.Id, product);
        }

        public Product GetById(string id)
        {
            return _repository.GetById(id);
        }

        public void Update(Product product)
        {
            _repository.Create(product.Id, product);
        }

        internal void LogoUploaded(string id, string fileName)
        {
            var p = GetById(id);
            p.LogoUrl = fileName;
            p.LogoUploadId = "";
            Update(p);
        }
    }


    public class ProductsController : ApiController
    {
       
        // GET: api/Products/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products
        public void Post([FromBody]ProductDto product)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]ProductDto product)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}
