using ENTech.Store.Entities.UnitOfWork;
using ENTech.Store.Infrastructure.Database;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.Commands;
using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;
using ENTech.Store.Services.SharedModule.Commands;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreCreateCommand : DbContextCommandBase<StoreCreateRequest, StoreCreateResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _storeRepository;
		private readonly IMapper _mapper;
		private readonly IInternalCommandService _internalCommandService;
		
		public StoreCreateCommand(IUnitOfWork unitOfWork, IRepository<Entities.StoreModule.Store> storeRepository, 
			IInternalCommandService internalCommandService, IMapper mapper)
			: base(unitOfWork.DbContext, false)
		{
			_storeRepository = storeRepository;
			_internalCommandService = internalCommandService;
			_mapper = mapper;
		}

		public override StoreCreateResponse Execute(StoreCreateRequest request)
		{
			var storeDto = request.Store;

			var addressCreateResponse = _internalCommandService.Execute<AddressCreateRequest, AddressCreateResponse, AddressCreateCommand>(new AddressCreateRequest
			{
				Address = _mapper.Map<AddressDto, AddressCreateDto>(storeDto.Address)
			});

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