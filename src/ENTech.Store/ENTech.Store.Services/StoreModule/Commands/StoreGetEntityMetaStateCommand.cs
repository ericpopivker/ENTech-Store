using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetEntityMetaStateCommand : CommandBase<StoreGetEntityMetaStateRequest, StoreGetEntityMetaStateResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _storeRepository;

		public StoreGetEntityMetaStateCommand(IDtoValidatorFactory dtoValidatorFactory, IRepository<Entities.StoreModule.Store> storeRepository)
			: base(dtoValidatorFactory, false)
		{
			_storeRepository = storeRepository;
		}

		public override StoreGetEntityMetaStateResponse Execute(StoreGetEntityMetaStateRequest request)
		{
			var entityMetaState = _storeRepository.GetEntityMetaState(request.Id);
			return new StoreGetEntityMetaStateResponse
			{
				EntityMetaState = entityMetaState
			};
		}
	}
}