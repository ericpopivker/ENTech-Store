using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.FileStorage;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using System;

namespace ENTech.Store.Services.UploadModule.Commands
{
    public class UploadUpdateCommand:DbContextCommandBase<UploadUpdateRequest, UploadUpdateResponse>
    {
        public UploadUpdateCommand(IUnitOfWork unitOfWork)
            : base(unitOfWork.DbContext, false)
		{
		}

        public override UploadUpdateResponse Execute(UploadUpdateRequest request)
        {
            const int mb = 1024 * 1024;

            //if (!_repository.Exists(id))
            //    CreateUpload(id);

            var u = _repository.GetById(id);
            if (u.OwnerId != this.UserId) throw new Exception("Access denied");

            if ((s.Length / mb) > (int)limit)
                throw new Exception("File is too big");

            u.FileExtension = request.Extension;
            u.Size = (int)s.Length;
            u.UpdatedAt = DateTime.UtcNow;

            _repository.Update(u.Id, u);

            var fileStorage = new LocalFileStorage();

            var url = await fileStorage.Save(u.Id + request.Extension, s, pos =>
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

            var upload = this.DbContext.Uploads.Create();
            upload.CreatedAt = DateTime.UtcNow;
            upload.LastUpdatedAt = upload.CreatedAt;
            upload.OwnerId = userId;
            
            //todo: UploadUpdateRequest upload

            return new UploadCreateResponse
			{
				IsSuccess = true,
                Id = upload.Id
			};
		}

    }
}



