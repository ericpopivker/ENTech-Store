using System.Linq;
using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.ProductModule.Dtos;
using ENTech.Store.Services.StoreModule.Expand.Dtos;

namespace ENTech.Store.Services.StoreModule.Expand.Strategies
{
	public class StoreProductsExpandStrategy : IDtoExpandStrategy<StoreExpandableDto>
	{
		private readonly IDtoLoaderFactory _dtoLoaderFactory;

		public StoreProductsExpandStrategy(IDtoLoaderFactory dtoLoaderFactory)
		{
			_dtoLoaderFactory = dtoLoaderFactory;
		}

		public void Apply(StoreExpandableDto store)
		{
			if (store.ProductIds != null && store.ProductIds.Any())
			{
				var addressLoader = _dtoLoaderFactory.Create<ProductDto>();
				var products = addressLoader.LoadMultiple(store.ProductIds);
				store.Products = products;
			}
		}
	}
}