using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.ProductModule.Validators.EntityValidators;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Commands
{
	public class StoreGetByIdCommand : CommandBase<StoreGetByIdRequest, StoreGetByIdResponse>
	{
		private readonly IRepository<Entities.StoreModule.Store> _storeRepository;
		private readonly IMapper _mapper;
		private readonly IStoreValidator _storeValidator;

		public StoreGetByIdCommand(ICachedRepository<Entities.StoreModule.Store> storeRepository, IStoreValidator storeValidator, IMapper mapper, IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
			_storeRepository = storeRepository;
			_mapper = mapper;
			_storeValidator = storeValidator;
		}

		public override StoreGetByIdResponse Execute(StoreGetByIdRequest request)
		{
			var entity = _storeRepository.GetById(request.Id);

			var dto = _mapper.Map<Entities.StoreModule.Store, StoreDto>(entity);

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