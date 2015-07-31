using System.Collections.Generic;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.GeoModule.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.StoreModule.Expand.Loaders
{
	public class AddressCommandDtoLoader : CommandDtoLoader<AddressGetByIdResponse, AddressFindByIdsResponse, AddressDto, StoreModule.Dtos.AddressDto>
	{
		public AddressCommandDtoLoader(IExternalCommandService externalCommandService, IMapper mapper)
			: base(externalCommandService, mapper)
		{
		}

		protected override IRequest<AddressGetByIdResponse> GetLoadRequest(int id)
		{
			return new AddressGetByIdRequest
			{
				Id = id
			};
		}

		protected override IRequest<AddressFindByIdsResponse> GetLoadMultipleRequest(IEnumerable<int> ids)
		{
			return new AddressFindByIdsRequest
			{
				Ids = ids
			};
		}
	}
}