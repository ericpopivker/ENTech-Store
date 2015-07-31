using System.Collections.Generic;

namespace ENTech.Store.Services.Expandable
{
	public interface IDtoLoader<T>
	{
		T Load(int id);
		IEnumerable<T> LoadMultiple(IEnumerable<int> ids);
	}
}