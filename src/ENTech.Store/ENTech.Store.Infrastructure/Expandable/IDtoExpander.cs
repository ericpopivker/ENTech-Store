using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Expandable
{
	public interface IDtoExpander
	{
		T LoadAndExpand<T>(int entityId, IEnumerable<ExpandOption<T>> expandOptions = null) where T : IExpandableDto;
	}
}