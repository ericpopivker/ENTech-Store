using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Services.Repositories;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Queries;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetEntityMetaStateCommand : DbContextCommandBase<StoreGetEntityMetaStateRequest, StoreGetEntityMetaStateResponse>
	{
		private IRepository<Entities.StoreModule.Store> _storeRepository;

		public StoreGetEntityMetaStateCommand(IUnitOfWork unitOfWork, IDtoValidatorFactory dtoValidatorFactory, IRepository<Entities.StoreModule.Store> storeRepository)
			: base(unitOfWork.DbContext, dtoValidatorFactory, false)
		{
			_storeRepository = storeRepository;
		}

		public override StoreGetEntityMetaStateResponse Execute(StoreGetEntityMetaStateRequest request)
		{
			var entityMetaState = _storeRepository.GetEntityMetaState(request.Id);
			return new StoreGetEntityMetaStateResponse {EntityMetaState = entityMetaState};
		}
	}
}