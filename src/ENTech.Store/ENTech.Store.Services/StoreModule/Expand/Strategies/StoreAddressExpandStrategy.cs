using ENTech.Store.Services.Expandable;
using ENTech.Store.Services.StoreModule.Dtos;
using ENTech.Store.Services.StoreModule.Expand.Dtos;

namespace ENTech.Store.Services.StoreModule.Expand.Strategies
{
	public class StoreAddressExpandStrategy : IDtoExpandStrategy<StoreExpandableDto>
	{
		private readonly IDtoLoaderFactory _dtoLoaderFactory;

		public StoreAddressExpandStrategy(IDtoLoaderFactory dtoLoaderFactory)
		{
			_dtoLoaderFactory = dtoLoaderFactory;
		}

		public void Apply(StoreExpandableDto store)
		{
			if (store.AddressId.HasValue)
			{
				var addressLoader = _dtoLoaderFactory.Create<AddressDto>();
				var address = addressLoader.Load(store.AddressId.Value);
				store.Address = address;
			}
		}
	}
}