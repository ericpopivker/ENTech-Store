using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using ENTech.Store.Api.DAL;
using System.Threading.Tasks;

namespace ENTech.Store.Api.v1
{

    //https://www.eventbrite.com/


    public class UploadDto
    {
        public string Id;
        public string OwnerId;//? need?
        public int Size;
        public int Uploaded;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
        public bool IsAttached;
    }


    [Route(ApiVersions.V1 + "/uploads")]
    public class UploadController : ApiController
    {
 
        private readonly BL.UploadFacade _uploader;
        private readonly BL.FileStorageBase _storage;

        public UploadController()
        {
            var dispatcher  = new BL.UploadEventDispatcher();
            BL.UploadSubscriber.SubscribeAllHandlers(dispatcher);
            _storage = new BL.LocalFileStorage();


            var currentUserId = "1";
            _uploader = new BL.UploadFacade(currentUserId, _storage, dispatcher);
        }


        [HttpPost]
        public async Task<UploadDto> CreateUpload()
        {
            //201
            var u = _uploader.CreateUpload(Guid.NewGuid().ToString());
            return new UploadDto
               {
                   Id = u.Id,
                   CreatedAt = u.CreatedAt,
                   UpdatedAt = u.UpdatedAt
               };
        }

        [HttpPost]
        [Route(ApiVersions.V1 + "/uploads/{id}")]
        public async Task Save([FromUri]Guid id)
        {
            using (var s = await this.Request.Content.ReadAsStreamAsync())
            {
                await _uploader.Save(id.ToString(), ".txt", BL.FileLimits.M1, s);
            }
        }

        [HttpGet]
        [Route(ApiVersions.V1 + "/uploads/{id}")]
        public UploadDto GetUpload(string id)
        {
            var u = _uploader.GetById(id);
            return new UploadDto
            {
                Id = u.Id,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            };
        }
    }


}

