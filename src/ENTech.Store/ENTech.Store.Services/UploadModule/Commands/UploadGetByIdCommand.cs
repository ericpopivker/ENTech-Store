using ENTech.Store.Entities;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.FileStorage;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using System;

namespace ENTech.Store.Services.UploadModule.Commands
{
    public class UploadGetByIdCommand : DbContextCommandBase<UploadGetByIdRequest, UploadGetByIdResponse>
    {
        public UploadGetByIdCommand(IUnitOfWork unitOfWork)
            : base(unitOfWork.DbContext, false)
        {
        }

        public override UploadGetByIdResponse Execute(UploadGetByIdRequest request)
        {
            //todo: rerieve from repository
            //request.Id
            return UploadGetByIdResponse
            {
            };

        }
    }
}
