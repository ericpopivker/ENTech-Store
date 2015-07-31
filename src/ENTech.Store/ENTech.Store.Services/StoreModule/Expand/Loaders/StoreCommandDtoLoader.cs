using System.Collections.Generic;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Expand.Dtos;
using ENTech.Store.Services.StoreModule.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Expand.Loaders
{
	public class StoreCommandDtoLoader : CommandDtoLoader<StoreGetByIdResponse, StoreFindByIdsResponse, StoreDto, StoreExpandableDto>
	{
		public StoreCommandDtoLoader(IExternalCommandService externalCommandService, IMapper mapper) : base(externalCommandService, mapper)
		{
		}

		protected override IRequest<StoreFindByIdsResponse> GetLoadMultipleRequest(IEnumerable<int> ids)
		{
			return new StoreFindByIdsRequest
			{
				Ids = ids
			};
		}

		protected override IRequest<StoreGetByIdResponse> GetLoadRequest(int id)
		{
			return new StoreGetByIdRequest
			{
				Id = id
			};
		}
	}
}