using ENTech.Store.Infrastructure;

namespace ENTech.Store.Services.Expandable
{
	public interface IDtoLoaderFactory
	{
		IDtoLoader<T> Create<T>();
	}

	public class DtoLoaderFactory : IDtoLoaderFactory
	{
		public IDtoLoader<T> Create<T>()
		{
			return IoC.Resolve<IDtoLoader<T>>();
		}
	}
}