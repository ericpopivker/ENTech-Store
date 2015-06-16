using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ENTech.Store.Api.v1
{
    public class UploadEntity
    {
        public string Id;
        public string OwnerId;
        public int Size;
        public int Uploaded;
        public DateTime CreationDate;
        public DateTime LastUpdateDate;
        public string EntityType;
        public string EntityId;
        public string URL;

        public bool IsProcessed;
        public bool IsAttached;
        public bool IsUploaded;
    }


    public class UploadFinishedEvent
    {
        public UploadEntity Info;
    }


    public class UploadRepository
    {
        public void Create(UploadEntity info)
        {
        }

        public void Update(UploadEntity info)
        {
        }

        public UploadEntity GetById(string id)
        {
        }
    }

    public interface IUploadEventPublisher
    {
        void Publish(UploadFinishedEvent e);
    }

    public class UploadService
    {
        public string UserId;
        private readonly UploadRepository _repository = new UploadRepository();
        private readonly IUploadEventPublisher _eventPublisher;

        public UploadService(IUploadEventPublisher eventPublisher)
        {
            this._eventPublisher = eventPublisher;
        }

        public UploadEntity CreateUpload()
        {
            var info = new UploadEntity
            {
                Id = Guid.NewGuid().ToString(),
                OwnerId = this.UserId,
                CreationDate = DateTime.UtcNow,
                LastUpdateDate = DateTime.UtcNow
            };

            _repository.Create(info);

            return info;
        }

        public async void Attach(string id, string entityType, string entityId)
        {
            var u = _repository.GetById(id);
            if (u.OwnerId != this.UserId) throw new Exception("Access denied");
            u.EntityId = entityId;
            u.EntityType = entityType;

            u.IsAttached = true;
            _repository.Update(u);

            if (u.IsUploaded)
            {
                _eventPublisher.Publish(new UploadFinishedEvent
                {
                    Info = u
                });
            }
        }

        public async void Save(string id, int fileSize, Stream s)
        {
            var u = _repository.GetById(id);
            if (u.OwnerId != this.UserId) throw new Exception("Access denied");
            u.Size = fileSize;
            u.LastUpdateDate = DateTime.UtcNow;

            _repository.Update(u);


            var b = new byte[1024 * 256];//256kb

            while (s.Position < s.Length)
            {
                await s.ReadAsync(b, 0, b.Length);
                //write to file / IFileWriter
                //update status
            }

            u.IsUploaded = true;
            _repository.Update(u);


            if (u.IsAttached)
            {
                _eventPublisher.Publish(new UploadFinishedEvent
                {
                    Info = u
                });
            }
        }
    }




    public class UploadDto
    {
        public string Id;
        public string OwnerId;//? need?
        public int Size;
        public int Uploaded;
        public DateTime CreationDate;
        public DateTime LastUpdateDate;
        public bool IsAttached;
    }


    [RoutePrefix(ApiVersions.V1 + "/uploads")]
    public class FileController : ApiController
    {
        private IUploadEventPublisher _publisher;
        private UploadService _service;

        public FileController()
        {
            _service = new UploadService(_publisher);
        }


        [HttpPost]
        [Route("")]
        public UploadDto CreateUpload()
        {
            //201
            var u = _service.CreateUpload(); 
            return new UploadDto
               {
                   Id = u.Id,
                   CreationDate = u.CreationDate,
                   LastUpdateDate = u.LastUpdateDate
               };

        }

        [HttpPost]
        [Route("/{id}")]
        public async void UploadFile(string id)
        {
            var s = await this.Request.Content.ReadAsStreamAsync();
            var w = await Request.Content.ReadAsMultipartAsync();
        }

        [HttpHead]
        [Route("{id}")]
        public object GetTokenInfo(string id)
        {
            var u = _service.Get(id);
            return new UploadDto
            {
                Id = u.Id,
                CreationDate = u.CreationDate,
                LastUpdateDate = u.LastUpdateDate
            };
        }
    }


}

