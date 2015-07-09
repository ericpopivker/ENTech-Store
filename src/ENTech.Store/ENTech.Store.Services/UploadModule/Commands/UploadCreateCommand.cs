using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using System;

namespace ENTech.Store.Services.UploadModule.Commands
{
    public class UploadCreateCommand:DbContextCommandBase<UploadCreateRequest, UploadCreateResponse>
    {
        public UploadCreateCommand(IUnitOfWork unitOfWork) : base(unitOfWork.DbContext, false)
		{
		}

        public override UploadCreateResponse Execute(UploadCreateRequest request)
        {
            var upload = this.DbContext.Uploads.Create();
            upload.CreatedAt = DateTime.UtcNow;
            upload.LastUpdatedAt = upload.CreatedAt;
            upload.Role = request.Role;
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



