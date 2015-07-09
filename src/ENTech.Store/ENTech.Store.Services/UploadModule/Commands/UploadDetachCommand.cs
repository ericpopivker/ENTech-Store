using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Entities.UploadModule;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using System;

namespace ENTech.Store.Services.UploadModule.Commands
{
    public class UploadDetachCommand : DbContextCommandBase<UploadDetachRequest, UploadDetachResponse>
    {
        public UploadDetachCommand(IUnitOfWork unitOfWork)
            : base(unitOfWork.DbContext, false)
		{
		}

        public override UploadDetachResponse Execute(UploadDetachRequest request)
        {
            if (request.UploadId > 0)
                throw new ArgumentNullException("uploadId");

            var u = (Upload)_repository.GetById(request.UploadId);
            if (u.OwnerId != this.UserId)
                throw new Exception("Access denied");

            u.AttachedEntityType = null;
            u.AttachedEntityFieldName = null;
            u.AttachedEntityId = 0;
            u.IsAttached = false;
            _repository.Update(u.Id, u);

            return new UploadDetachResponse
			{
				IsSuccess = true
			};
		}

    }
}



