using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.GeoModule.EntityValidators;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Commands
{
	public class AddressGetByIdCommand : CommandBase<AddressGetByIdRequest, AddressGetByIdResponse>
	{
		private readonly IRepository<Address> _addressRepository;
		private readonly IMapper _mapper;
		private readonly IAddressValidator _addressValidator;

		public AddressGetByIdCommand(IRepository<Address> addressRepository,  IAddressValidator addressValidator, IMapper mapper, IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
			_addressRepository = addressRepository;
			_mapper = mapper;
			_addressValidator = addressValidator;
		}

		public override AddressGetByIdResponse Execute(AddressGetByIdRequest request)
		{
			var address = _addressRepository.GetById(request.Id);

			var dto = _mapper.Map<Address, AddressDto>(address);

			return new AddressGetByIdResponse
			{
				Item = dto
			};
		}

		protected override void ValidateRequestInternal(AddressGetByIdRequest request, ValidateRequestResult<AddressGetByIdRequest> validateRequestResult)
		{
			if (validateRequestResult.NoErrorsForArgument(req => request.Id))
			{
				var result = _addressValidator.ValidateId(request.Id);

				if (!result.IsValid)
					validateRequestResult.AddArgumentError(req => req.Id, result.ArgumentError);
			}
		}
	}
}