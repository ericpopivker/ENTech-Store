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
		private readonly IRepository<Country> _countryRepository;
		private readonly IRepository<State> _stateRepository;

		public StoreCreateCommand(IUnitOfWork unitOfWork, 
			IRepository<Entities.StoreModule.Store> storeRepository, 
			IRepository<Country> countryRepository, 
			IRepository<State> stateRepository)
			: base(unitOfWork.DbContext, false)
		{
			_storeRepository = storeRepository;
			_countryRepository = countryRepository;
			_stateRepository = stateRepository;
		}

		public override StoreCreateResponse Execute(StoreCreateRequest request)
		{
			var storeDto = request.Store;

			var entity = new Entities.StoreModule.Store
			{
				Name = storeDto.Name,
				Logo = storeDto.Logo,
				Phone = storeDto.Phone,
				Email = storeDto.Email,
				Address = new Address
				{
					City = storeDto.Address.City,
					Country = _countryRepository.GetById(storeDto.Address.CountryId),
					State = storeDto.Address.StateId.HasValue ? _stateRepository.GetById(storeDto.Address.StateId.Value) : null,
					StateOther = storeDto.Address.StateOther,
					Street = storeDto.Address.Street,
					Street2 = storeDto.Address.Street2,
					Zip = storeDto.Address.Zip
				},
				TimezoneId = storeDto.Timezone
			};

			_storeRepository.Add(entity);


			return new StoreCreateResponse
			{
				IsSuccess = true
			};
		}
	}
}