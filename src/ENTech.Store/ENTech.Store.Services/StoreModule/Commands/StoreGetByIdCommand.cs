using System.IO;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Repositories;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Queries;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetByIdCommand : DbContextCommandBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		private IRepository<Entities.StoreModule.Store> _storeRepository;
		public StoreGetByIdCommand(IUnitOfWork unitOfWork, IDtoValidatorFactory dtoValidatorFactory, IRepository<Entities.StoreModule.Store> storeRepository)
			: base(unitOfWork.DbContext, dtoValidatorFactory, false)
		{
			_storeRepository = storeRepository;
		}

		public override StoreGetByIdResponse Execute(StoreGetByIdRequest request)
		{
			Entities.StoreModule.Store store = _storeRepository.GetById(request.Id);
			StoreDto storeDto = new StoreDto();
			
			//map store to storeDto

			return new StoreGetByIdResponse {Item = storeDto};
		}
	}
}