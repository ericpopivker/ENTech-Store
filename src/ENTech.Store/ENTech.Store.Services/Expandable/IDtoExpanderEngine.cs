using System;
using ENTech.Store.Services.StoreModule.Expand.Configurations;
using ENTech.Store.Services.StoreModule.Expand.Dtos;

namespace ENTech.Store.Services.Expandable
{
	public interface IDtoExpanderEngine
	{
		ExpandConfiguration GetConfiguration(Type expandableDtoType);
	}

	public  class DtoExpanderEngine : IDtoExpanderEngine
	{
		public ExpandConfiguration GetConfiguration(Type expandableDtoType)
		{
			if (expandableDtoType == typeof (StoreExpandableDto))
				return new StoreExpandableDtoExpandProfile().GetConfiguration();

			if (expandableDtoType == typeof(ProductExpandableDto))
				return new ProductExpandableDtoExpandProfile().GetConfiguration();

			throw new NotImplementedException();
		}
	}
}