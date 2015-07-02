using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ENTech.Store.Api.DAL;

namespace ENTech.Store.Api.BL
{
    
    public class UploadFinishedEvent
    {
        public string UploadId;
        public string Url;
        public string AttachedEntityType;
        public string AttachedEntityId;
        public string AttachedEntityFieldName;
    }

    public interface IUploadedEventHandler
    {
        void OnUploaded(string uploadId, string id, string fieldName, string url);
    }

    public interface IUploadEventPublisher
    {
        void Publish(UploadFinishedEvent e);
    }

    public enum FileLimits
    {
        M1 = 1,
        M5 = 5,
        M10 = 10,
        M20 = 20
    }

    public class UploadFacade
    {
        public readonly string UserId;
        private readonly UploadRepository _repository = new UploadRepository();
        private readonly IUploadEventPublisher _eventPublisher;
        private readonly FileStorageBase _fileStorage;

        public UploadFacade(string userId, FileStorageBase fileStorage, IUploadEventPublisher eventPublisher)
        {
            this.UserId = userId;
            this._fileStorage = fileStorage;
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

        public async void Attach(string uploadId, string entityType, string entityFieldname, string entityId)
        {
            if (String.IsNullOrEmpty(uploadId))
                throw new ArgumentNullException("uploadId");

            var u = _repository.GetById(uploadId);
            if (u.OwnerId != this.UserId)
                throw new Exception("Access denied");

            u.AttachedEntityType = entityType;
            u.AttachedEntityFieldName = entityFieldname;
            u.AttachedEntityId = entityId;
            u.IsAttached = true;
            _repository.Update(u.Id, u);

            if (u.IsUploaded)
                Finish(u);
        }

        public async Task Save(string id, string fileExtension, FileLimits limit, Stream s)
        {
            const int mb = 1024 * 1024;

            if ((s.Length / mb) > (int)limit)
                throw new Exception("File is too big");

            if (!_repository.Exists(id))
                CreateUpload(id);

            var u = _repository.GetById(id);
            if (u.OwnerId != this.UserId) throw new Exception("Access denied");

            u.FileExtension = fileExtension;
            u.Size = (int)s.Length;
            u.UpdatedAt = DateTime.UtcNow;

            _repository.Update(u.Id, u);

            var url = await _fileStorage.Save(u.Id + fileExtension, s, pos =>
            {
                //update status
                u.Uploaded = s.Position;
                _repository.Update(u.Id, u);
            });

            u.URL = url;
            u.IsUploaded = true;
            _repository.Update(u.Id, u);

            if (u.IsAttached)
                Finish(u);
        }

        private void Finish(FileUpload u)
        {
            _eventPublisher.Publish(new UploadFinishedEvent
            {
                UploadId = u.Id,
                Url = u.URL,
                AttachedEntityId = u.AttachedEntityId,
                AttachedEntityType = u.AttachedEntityType,
                AttachedEntityFieldName = u.AttachedEntityFieldName
            });

            //delete the upload record
            _repository.Delete(u.Id);
        }
    }

}
