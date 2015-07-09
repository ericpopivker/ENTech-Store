using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.SharedModule.Requests;
using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using System;

namespace ENTech.Store.Services.UploadModule.Commands
{
    public class UploadFinishCommand : DbContextCommandBase<UploadFinishRequest, UploadFinishResponse>
    {
        public UploadFinishCommand(IUnitOfWork unitOfWork)
            : base(unitOfWork.DbContext, false)
        {
        }

        public override UploadFinishResponse Execute(UploadFinishRequest request)
        {
            //this.DbContext.Uploads.
            _repository.Delete(request.UploadId);
            //move to cdn here.

            var cdnUrl = "";

            EntityFinishUploadRequest r = new EntityFinishUploadRequest
            {
                EntityId = request.AttachedEntityId,
                EntityFieldName = request.AttachedEntityFieldName,
                CdnUrl = cdnUrl
            }; 
            
            var cmd = GetCommand(request.AttachedEntityType);

            var result = new UploadFinishResponse
            {
                IsSuccess = false
            };

            if (cmd != null )
            {
                var cmdResult = cmd.Execute(r);
                result.IsSuccess = cmdResult.IsSuccess;
                result.Error = cmdResult.Error;
                result.ArgumentErrors = cmdResult.ArgumentErrors;
            }

            return result;
        }

        private static DbContextCommandBase<EntityFinishUploadRequest, UploadFinishResponse> GetCommand(string entityTypeName)
        {
            if (entityTypeName == "Product")
            {
                //todo: use command service
                return new ProductModule.Commands.ProductFinishUploadCommand();
            }

            return null;
        }
    }
}



