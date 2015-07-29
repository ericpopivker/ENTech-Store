using System;

namespace ENTech.Store.Infrastructure.Expandable
{
	public class DtoExpandStrategyFactory : IDtoExpandStrategyFactory
	{
		public IDtoExpandStrategy<T> Create<T>(ExpandOption<T> expandOptions)
		{
			throw new NotImplementedException();
		}
	}
}