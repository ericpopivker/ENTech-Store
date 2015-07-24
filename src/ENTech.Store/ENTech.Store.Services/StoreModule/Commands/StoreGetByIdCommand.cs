using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.GeoModule.Commands;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;
using AddressDto = ENTech.Store.Services.GeoModule.Dtos.AddressDto;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetByIdCommand : CommandBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _storeRepository;
		private readonly IMapper _mapper;
		private readonly IStoreValidator _storeValidator;
		private readonly IInternalCommandService _internalCommandService;

		public StoreGetByIdCommand(IRepository<Entities.StoreModule.Store> storeRepository, IStoreValidator storeValidator, IMapper mapper, IInternalCommandService internalCommandService, IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
			_storeRepository = storeRepository;
			_mapper = mapper;
			_internalCommandService = internalCommandService;
			_storeValidator = storeValidator;
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

				var responseAddress = addressGetByIdResponse.Item;

				dto.Address = _mapper.Map<AddressDto, Dtos.AddressDto>(responseAddress);
			}

			return new StoreGetByIdResponse
			{
				Item = dto
			};
		}

		protected override void ValidateRequestInternal(StoreGetByIdRequest request, ValidateRequestResult<StoreGetByIdRequest> validateRequestResult)
		{
			if (validateRequestResult.NoErrorsForArgument(req => request.Id))
			{
				var result = _storeValidator.ValidateId(request.Id);

				if (!result.IsValid)
					validateRequestResult.AddArgumentError(req => req.Id, result.ArgumentError);
			}
		}
	}
}