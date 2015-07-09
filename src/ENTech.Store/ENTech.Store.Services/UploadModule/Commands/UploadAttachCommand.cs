using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Entities.UploadModule;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using System;

namespace ENTech.Store.Services.UploadModule.Commands
{
    public class UploadAttachCommand : DbContextCommandBase<UploadAttachRequest, UploadAttachResponse>
    {
        public UploadAttachCommand(IUnitOfWork unitOfWork)
            : base(unitOfWork.DbContext, false)
		{
		}

        public override UploadAttachResponse Execute(UploadAttachRequest request)
        {
            if (request.UploadId > 0)
                throw new ArgumentNullException("uploadId");

            var u = (Upload)_repository.GetById(uploadId);
            if (u.OwnerId != this.UserId)
                throw new Exception("Access denied");

            u.AttachedEntityType = request.AttachingEntityType;
            u.AttachedEntityFieldName = request.AttachingEntityFieldName;
            u.AttachedEntityId = request.AttachingEntityId;
            u.IsAttached = true;
            _repository.Update(u.Id, u);

            if (u.IsUploaded)
                Finish(u);

            return new UploadAttachResponse
			{
				IsSuccess = true
			};
		}

    }
}



