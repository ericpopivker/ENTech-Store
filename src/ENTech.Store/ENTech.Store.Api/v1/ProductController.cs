using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ENTech.Store.Api.DAL;
using Newtonsoft.Json;

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

        public UploadReferenceDto(string id, int statusPercentage)
            : base("Upload")
        {
            this.Id = id;
            this.StatusPercentage = statusPercentage;
        }
    }

    public class UrlDto : FileBaseDto
    {
        public string Url;

        public UrlDto(string url)
            : base("Url")
        {
            this.Url = url;
        }
    }


    public class ProductResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public FileBaseDto Logo { get; set; }
    }

    public class ProductCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoUploadId { get; set; }
    }




    public class ProductsController : ApiController
    {
        private readonly BL.UploadFacade _uploader;
        private readonly BL.FileStorageBase _storage;

        public ProductsController()
        {
            var dispatcher = new BL.UploadEventDispatcher();
            BL.UploadSubscriber.SubscribeAllHandlers(dispatcher);
            _storage = new BL.LocalFileStorage();

            var currentUserId = "1";
            _uploader = new BL.UploadFacade(currentUserId, _storage, dispatcher);
        }

        // GET: api/Products/5
        [Route(ApiVersions.V1 + "/products/{id}")]
        [HttpGet]
        public async Task<ProductResponse> Get(string id)
        {
            var productService = new BL.ProductFacade();
            var p = productService.GetById(id);

            var response = new ProductResponse
            {
                Id = p.Description,
                Title = p.Title
            };

            if (!string.IsNullOrEmpty(p.LogoUploadId))
            {
                var upload = _uploader.GetById(p.LogoUploadId);
                var percentage = (int)upload.Uploaded * 100 / upload.Size;
                response.Logo = new UploadReferenceDto(upload.Id, percentage);
            }
            else if (!string.IsNullOrEmpty(p.LogoUrl))
                response.Logo = new UrlDto(p.LogoUrl);
            else
                response.Logo = null;

            return response;
        }

        [Route(ApiVersions.V1 + "/products")]
        [HttpPost]
        public async Task<ProductResponse> Post(ProductCreateRequest product)
        {
            await Task.Delay(5000);

            var productService = new BL.ProductFacade();
            var p = new DAL.Product()
            {
                Id = "1",//..Guid.NewGuid().ToString(),
                Title = product.Title,
                LogoUploadId = product.LogoUploadId,
                Description = product.Description
            };

            productService.Create(p);

            if (!string.IsNullOrEmpty(product.LogoUploadId))
                _uploader.Attach(product.LogoUploadId, "Product", "Logo", p.Id);

            return await this.Get(p.Id);
        }
    }
}
