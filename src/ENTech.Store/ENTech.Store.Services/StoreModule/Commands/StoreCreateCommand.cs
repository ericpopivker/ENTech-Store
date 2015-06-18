using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Database;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreCreateCommand : DbContextCommandBase<StoreCreateRequest, StoreCreateResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _storeRepository;

		public StoreCreateCommand(IUnitOfWork unitOfWork, 
			IRepository<Entities.StoreModule.Store> storeRepository)
			: base(unitOfWork.DbContext, false)
		{
			_storeRepository = storeRepository;
		}

		public override StoreCreateResponse Execute(StoreCreateRequest request)
		{
			var storeDto = request.Store;

			_storeRepository.Add(new Entities.StoreModule.Store
			{
				Name = storeDto.Name
			});

			return new StoreCreateResponse
			{
				IsSuccess = true
			};
		}
	}
}