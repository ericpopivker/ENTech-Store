using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.Commands;
using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using AddressDto = ENTech.Store.Services.StoreModule.Dtos.AddressDto;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreCreateCommand : CommandBase<StoreCreateRequest, StoreCreateResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _storeRepository;
		private readonly IMapper _mapper;
		private readonly IInternalCommandService _internalCommandService;
		
		public StoreCreateCommand(IRepository<Entities.StoreModule.Store> storeRepository, 
			IInternalCommandService internalCommandService, IMapper mapper)
			: base(false)
		{
			_storeRepository = storeRepository;
			_internalCommandService = internalCommandService;
			_mapper = mapper;
		}

		public override StoreCreateResponse Execute(StoreCreateRequest request)
		{
			var storeDto = request.Store;

			var addressCreateRequest = new AddressCreateRequest
			{
				Address = _mapper.Map<AddressDto, AddressCreateOrUpdateDto>(storeDto.Address),
				ApiKey = request.ApiKey
			};

			var addressCreateResponse = _internalCommandService.Execute<AddressCreateRequest, AddressCreateResponse, AddressCreateCommand>(addressCreateRequest);

			if (addressCreateResponse.IsSuccess == false)
				return InternalServerError();

			var entity = new Entities.StoreModule.Store
			{
				Name = storeDto.Name,
				Logo = storeDto.Logo,
				Phone = storeDto.Phone,
				Email = storeDto.Email,
				AddressId = addressCreateResponse.AddressId,
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