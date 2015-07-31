using System.Collections.Generic;
using ENTech.Store.Infrastructure.Mapping;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.ProductModule.Responses;

namespace ENTech.Store.Services.StoreModule.Expand.Loaders
{
	public class ProductCommandDtoLoader : CommandDtoLoader<ProductGetByIdResponse, ProductFindByIdsResponse, ProductDto, ProductDto>
	{
		public ProductCommandDtoLoader(IExternalCommandService externalCommandService, IMapper mapper)
			: base(externalCommandService, mapper)
		{
		}

		protected override IRequest<ProductGetByIdResponse> GetLoadRequest(int id)
		{
			return new ProductGetByIdRequest
			{
				Id = id
			};
		}

		protected override IRequest<ProductFindByIdsResponse> GetLoadMultipleRequest(IEnumerable<int> ids)
		{
			return new ProductFindByIdsRequest
			{
				Ids = ids
			};
		}
	}
}