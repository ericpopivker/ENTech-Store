using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Commands;
using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Commands
{
	public class AddressGetByIdCommand : CommandBase<AddressGetByIdRequest, AddressGetByIdResponse>
	{
		private readonly IRepository<Address> _addressRepository;
		private readonly IMapper _mapper;

		public AddressGetByIdCommand(IRepository<Address> addressRepository, IMapper mapper) : base(false)
		{
			_addressRepository = addressRepository;
			_mapper = mapper;
		}

		public override AddressGetByIdResponse Execute(AddressGetByIdRequest request)
		{
			var address = _addressRepository.GetById(request.Id);

			var dto = _mapper.Map<Address, AddressDto>(address);

			return new AddressGetByIdResponse
			{
				Item = dto,
				IsSuccess = true
			};
		}
	}
}