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

    public class UploadFinishedEvent
    {
        public string Id;
        public string FileName;
        public string AttachedEntityType;
        public string AttachedEntityId;
        public string AttachedEntityFieldName;
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

        public FileUpload CreateUpload(string id)
        {
            var info = new FileUpload
            {
                Id = id,
                OwnerId = this.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _repository.Create(info.Id, info);

            return info;
        }

        public async void Attach(string id, string entityType, string entityFieldname, string entityId)
        {
            var u = _repository.GetById(id);
            if (u.OwnerId != this.UserId)
                throw new Exception("Access denied");

            u.AttachedEntityType = entityType;
            u.AttachedEntityFieldName = entityFieldname;
            u.AttachedEntityId = entityId;
            u.IsAttached = true;
            _repository.Update(u.Id, u);

            if (u.IsUploaded)
                EmitUploaded(u);
        }

        public async Task Save(string id, string fileName, int fileSize, Stream s)
        {
            if (!_repository.Exists(id))
                CreateUpload(id);


            var u = _repository.GetById(id);
            if (u.OwnerId != this.UserId) throw new Exception("Access denied");

            u.FileName = fileName;
            u.Size = fileSize;
            u.UpdatedAt = DateTime.UtcNow;

            _repository.Update(u.Id, u);

            var b = new byte[1024 * 256];//256kb

            var path = @"M:\_tempo\Files\" + fileName;

            using (var fs = new FileStream(path, FileMode.CreateNew))
            {
                while (s.Position < s.Length)
                {
                    //write to file / IFileWriter
                    var actualLen = await s.ReadAsync(b, 0, b.Length);
                    await fs.WriteAsync(b, 0, actualLen);

                    //update status
                    u.Uploaded = s.Position;
                    _repository.Update(u.Id, u);
                }

                await fs.FlushAsync();
            }

            u.IsUploaded = true;
            _repository.Update(u.Id, u);

            if (u.IsAttached)
                EmitUploaded(u);
        }

        private void EmitUploaded(FileUpload u)
        {
            _eventPublisher.Publish(new UploadFinishedEvent
            {
                Id = u.Id,
                AttachedEntityId = u.AttachedEntityId,
                AttachedEntityType = u.AttachedEntityType,
                AttachedEntityFieldName = u.AttachedEntityFieldName,
                FileName = u.FileName
            });
        }
    }



    public class UploadEventDispatcher : IUploadEventPublisher
    {
        private readonly UploadRepository _repository = new UploadRepository();

        public void Publish(UploadFinishedEvent e)
        {
            IUploadedEventHandler h = FindHandler(e.AttachedEntityType);
            if (h == null)
                throw new Exception("Handler not found");

            //move to cdn goes here
            var url = "cdn-" + e.FileName;

            h.OnUploaded(e.Id, e.AttachedEntityId, e.AttachedEntityFieldName, url);

            //delete the upload record
            _repository.Delete(e.Id);
        }

        private IUploadedEventHandler FindHandler(string entityType)
        {
            if (entityType.StartsWith("Product"))
                return new ProductService();

            return null;
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
            var u = _service.CreateUpload(Guid.NewGuid().ToString());
            return new UploadDto
               {
                   Id = u.Id,
                   CreatedAt = u.CreatedAt,
                   UpdatedAt = u.UpdatedAt
               };
        }

        [HttpPost]
        [Route("{id}")]
        public async void Upload(string id)
        {
            var context = HttpContext.Current;

            //var w = await Request.Content.ReadAsMultipartAsync();
            var publisher = new UploadEventDispatcher();
            var uploadService = new UploadService(publisher);

            var fileSize = Convert.ToInt32(this.Request.Content.Headers.ContentLength);
            var s = await this.Request.Content.ReadAsStreamAsync();
            await uploadService.Save(id, id, fileSize, s);
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

