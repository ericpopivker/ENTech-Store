using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Commands
{
	public class AddressGetEntityMetaStateCommand : CommandBase<AddressGetEntityMetaStateRequest, AddressGetEntityMetaStateResponse>
	{
		private readonly IRepository<Address> _addressRepository;

		public AddressGetEntityMetaStateCommand(IRepository<Address> addressRepository, IDtoValidatorFactory dtoValidatorFactory) : base(dtoValidatorFactory, false)
		{
			_addressRepository = addressRepository;
		}

		public override AddressGetEntityMetaStateResponse Execute(AddressGetEntityMetaStateRequest request)
		{
			var entityMetaState = _addressRepository.GetEntityMetaState(request.Id);
			return new AddressGetEntityMetaStateResponse
			{
				EntityMetaState = entityMetaState
			};
		}
	}
}