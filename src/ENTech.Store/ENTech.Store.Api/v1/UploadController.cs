using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ENTech.Store.Api.DAL;

namespace ENTech.Store.Api.v1
{

    //https://www.eventbrite.com/

    public class UploadFinishedEvent
    {   
        public string Id;
        public string FileName;
        public string AttachedEntityType;
        public string AttachedEntityId;
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


        public FileUpload GetById(string id)
        {
            return _repository.GetById(id);
        }

        public FileUpload CreateUpload()
        {
            var info = new FileUpload
            {
                Id = Guid.NewGuid().ToString(),
                OwnerId = this.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _repository.Create(info.Id, info);

            return info;
        }

        public async void Attach(string id, string entityType, string entityId)
        {
            var u = _repository.GetById(id);
            if (u.OwnerId != this.UserId) 
                   throw new Exception("Access denied");
            
            u.AttachedEntityType = entityType;
            u.AttachedEntityId = entityId;
            u.IsAttached = true;
            _repository.Update(u.Id, u);

            if (u.IsUploaded)
            {
                _eventPublisher.Publish(new UploadFinishedEvent
                {
                    Id=u.Id,
                    AttachedEntityId = u.AttachedEntityId,
                    AttachedEntityType = u.AttachedEntityType,
                    FileName = u.FileName
                });
            }
        }

        public async void Save(string id, string fileName, int fileSize, Stream s)
        {
            var u = _repository.GetById(id);
            if (u.OwnerId != this.UserId) throw new Exception("Access denied");

            u.FileName = fileName;
            u.Size = fileSize;
            u.UpdatedAt = DateTime.UtcNow;

            _repository.Update(u.Id, u);


            var b = new byte[1024 * 256];//256kb

            while (s.Position < s.Length)
            {
                await s.ReadAsync(b, 0, b.Length);
                //write to file / IFileWriter
                //update status
            }

            u.IsUploaded = true;
            _repository.Update(u.Id, u);


            if (u.IsAttached)
            {
                _eventPublisher.Publish(new UploadFinishedEvent
                {
                    Id = u.Id,
                    AttachedEntityId = u.AttachedEntityId,
                    AttachedEntityType = u.AttachedEntityType,
                    FileName = u.FileName
                });
            }
        }
    }



    public class UploadEventDispatcher : IUploadEventPublisher
    {
        public ProductService ProductService;

        public void Publish(UploadFinishedEvent e)
        {
            if (e.AttachedEntityType.StartsWith("Product"))
            {
                ProductService.LogoUploaded(e.AttachedEntityId, e.FileName);
            }
        }
    }


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


    [RoutePrefix(ApiVersions.V1 + "/uploads")]
    public class UploadController : ApiController
    {
        private IUploadEventPublisher _publisher;
        private UploadService _service;

        public UploadController()
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
                   CreatedAt = u.CreatedAt,
                   UpdatedAt = u.UpdatedAt
               };

        }

        [HttpPost]
        [Route("/{id}")]
        public async void Upload(string id)
        {
            var s = await this.Request.Content.ReadAsStreamAsync();
            var w = await Request.Content.ReadAsMultipartAsync();
        }

        [HttpHead]
        [Route("{id}")]
        public object GetUpload(string id)
        {
            var u = _service.GetById(id);
            return new UploadDto
            {
                Id = u.Id,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            };
        }
    }


}

