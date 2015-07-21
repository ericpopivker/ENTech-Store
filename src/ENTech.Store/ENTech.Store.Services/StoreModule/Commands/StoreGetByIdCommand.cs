using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.Commands;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetByIdCommand : CommandBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		private readonly IInternalCommandService _internalCommandService;
		private readonly IRepository<Entities.StoreModule.Store> _storeRepository;
		private readonly IMapper _mapper;

		public StoreGetByIdCommand(IRepository<Entities.StoreModule.Store> storeRepository, IMapper mapper, IInternalCommandService internalCommandService)
			: base(false)
		{
			_storeRepository = storeRepository;
			_mapper = mapper;
			_internalCommandService = internalCommandService;
		}

		public override StoreGetByIdResponse Execute(StoreGetByIdRequest request)
		{
			var entity = _storeRepository.GetById(request.Id);

			var dto = _mapper.Map<Entities.StoreModule.Store, StoreDto>(entity);

			if (entity.AddressId.HasValue)
			{

				var addressGetByIdResponse =
					_internalCommandService.Execute<AddressGetByIdRequest, AddressGetByIdResponse, AddressGetByIdCommand>(new AddressGetByIdRequest
					{
						ApiKey = request.ApiKey,
						Id = entity.AddressId.Value
					});

				if (addressGetByIdResponse == null || addressGetByIdResponse.IsSuccess == false)
					return InternalServerError();

				var responseAddress = addressGetByIdResponse.Item;

				dto.Address = _mapper.Map<GeoModule.Dtos.AddressDto, AddressDto>(responseAddress);
			}

			return new StoreGetByIdResponse
			{
				IsSuccess = true,
				Item = dto
			};
		}
	}
}