using System;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Commands
{
	public class AddressCreateCommand : CommandBase<AddressCreateRequest, AddressCreateResponse>
	{
		private readonly IRepository<Address> _addressRepository;
		private IMapper _mapper;

		public AddressCreateCommand(IRepository<Address> addressRepository, IMapper mapper, IDtoValidatorFactory dtoValidatorFactory)
			: base(dtoValidatorFactory, false)
		{
			_addressRepository = addressRepository;
			_mapper = mapper;
		}

		public override AddressCreateResponse Execute(AddressCreateRequest request)
		{
			var entity = request.Address;

			var domainEntity = new Address
			{
				City = entity.City,
				CountryId = entity.CountryId,
				StateId = entity.StateId,
				StateOther = entity.StateOther,
				Street = entity.Street,
				Street2 = entity.Street2,
				Zip = entity.Zip
			};

			_addressRepository.Add(domainEntity);

			return new AddressCreateResponse
			{
				AddressId = domainEntity.Id
			};
		}
	}
}