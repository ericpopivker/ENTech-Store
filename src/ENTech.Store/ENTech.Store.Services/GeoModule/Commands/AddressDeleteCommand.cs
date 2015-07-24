using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.GeoModule.EntityValidators;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Commands
{
	public class AddressDeleteCommand : CommandBase<AddressDeleteRequest, AddressDeleteResponse>
	{
		private IRepository<Address> _addressRepository;
		private IAddressValidator _addressValidator;

		public AddressDeleteCommand(IRepository<Address> addressRepository, IAddressValidator addressValidator,
			IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
			_addressRepository = addressRepository;
			_addressValidator = addressValidator;
		}

		public override AddressDeleteResponse Execute(AddressDeleteRequest request)
		{
			var address = _addressRepository.GetById(request.AddressId);

			_addressRepository.Delete(address);

			return new AddressDeleteResponse();
		}

		protected override void ValidateRequestInternal(AddressDeleteRequest request, ValidateRequestResult<AddressDeleteRequest> validateRequestResult)
		{
			if (validateRequestResult.NoErrorsForArgument(req => request.AddressId))
			{
				var result = _addressValidator.ValidateId(request.AddressId);

				if (!result.IsValid)
					validateRequestResult.AddArgumentError(req => req.AddressId, result.ArgumentError);
			}
		}
	}
}