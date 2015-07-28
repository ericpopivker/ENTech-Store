using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ENTech.Store.Services.Expandable
{
	public class DtoExpander : IDtoExpander
	{
		private readonly IDtoLoaderFactory _dtoLoaderFactory;
		private readonly IDtoExpandStrategyFactory _dtoExpandStrategyFactory;

		public DtoExpander(IDtoLoaderFactory dtoLoaderFactory, IDtoExpandStrategyFactory dtoExpandStrategyFactory)
		{
			_dtoLoaderFactory = dtoLoaderFactory;
			_dtoExpandStrategyFactory = dtoExpandStrategyFactory;
		}

		public T LoadAndExpand<T>(int entityId, IEnumerable<ExpandOption<T>> expandOptions) where T : IExpandableDto
		{
			var loader = _dtoLoaderFactory.Create<T>();

			var result = loader.Load(entityId);

			if (expandOptions != null)
			{
				ICollection<IDtoExpandStrategy<T>> expandStrategies = new Collection<IDtoExpandStrategy<T>>();

				foreach (var expandOption in expandOptions)
				{
					expandStrategies.Add(_dtoExpandStrategyFactory.Create(expandOption));
				}

				foreach (var expansionStrategy in expandStrategies)
				{
					expansionStrategy.Apply(result);
				}
			}

			return result;
		}
	}
}