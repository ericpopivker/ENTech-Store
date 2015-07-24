using System;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Commands
{
	public class AddressUpdateCommand : CommandBase<AddressUpdateRequest, AddressUpdateResponse>
	{
		private readonly IRepository<Address> _addressRepository;

		public AddressUpdateCommand(IRepository<Address> addressRepository, IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
			_addressRepository = addressRepository;
		}

		public override AddressUpdateResponse Execute(AddressUpdateRequest request)
		{
			var address = _addressRepository.GetById(request.AddressId);

			var addressChanges = request.Address;

			address.City = addressChanges.City;
			address.CountryId = addressChanges.CountryId;
			address.StateId = addressChanges.StateId;
			address.CountryId = addressChanges.CountryId;
			address.Street = addressChanges.Street;
			address.Street2 = addressChanges.Street2;
			address.Zip = addressChanges.Zip;

			_addressRepository.Update(address);

			return new AddressUpdateResponse();
		}
	}
}