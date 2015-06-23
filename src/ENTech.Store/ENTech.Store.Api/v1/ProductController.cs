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

    public class UploadReferenceDto : FileBaseDto
    {
        public string Id;
        public int StatusPercentage;

        public UploadReferenceDto(string id)
            : base("Upload")
        {
            this.Id = id;
        }
    }

    public class UrlDto : FileBaseDto
    {
        public string Url;

        public UrlDto(string url)
            : base("Upload")
        {
            this.Url = url;
        }
    }


    public class ProductDto
    {
        public string Id;
        public string Title;
        public string Description;
        public FileBaseDto Logo;
    }

    public class ProductCreateRequest
    {
        public string Title;
        public string Description;
        public string LogoUploadId;
    }


    public interface IUploadedEventHandler
    {
        void OnUploaded(string uploadId, string id, string fieldName, string url);
    }

    public class ProductService : IUploadedEventHandler
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

        public void OnUploaded(string uploadId, string entityId, string fieldName, string url)
        {
            if (fieldName.ToLower() == "logo")
            {
                var p = GetById(entityId);

                if (p.LogoUploadId == uploadId)
                {
                    p.LogoUrl = url;
                    p.LogoUploadId = "";
                    Update(p);
                }//else - the upload is not actual (was canceled before)
            }

        }
    }



    [RoutePrefix(ApiVersions.V1 + "/products")]
    public class ProductsController : ApiController
    {
        // GET: api/Products/5
        [Route("{id}")]
        [HttpGet]
        public ProductDto Get(string id)
        {
            var productService = new ProductService();
            var p = productService.GetById(id);

            return new ProductDto
            {
                Id = p.Description,
                Title = p.Title,
                Logo = (string.IsNullOrEmpty(p.LogoUploadId) ? ((FileBaseDto)new UrlDto(p.LogoUrl)) : ((FileBaseDto)new UploadReferenceDto(p.LogoUploadId)))

            };
        }

        // POST: api/Products

        [Route("")]
        [HttpPost]
        public ProductDto Post(ProductCreateRequest product)
        {
            var publisher = new UploadEventDispatcher();
            var uploadService = new UploadService(publisher);

            var productService = new ProductService();
            var p = new DAL.Product()
            {
                Id = Guid.NewGuid().ToString(),
                Title = product.Title,
                LogoUploadId = product.LogoUploadId,
                Description = product.Description
            };

            productService.Create(p);
            uploadService.Attach(product.LogoUploadId, "Product", "Logo", p.Id);

            return this.Get(p.Id);
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
