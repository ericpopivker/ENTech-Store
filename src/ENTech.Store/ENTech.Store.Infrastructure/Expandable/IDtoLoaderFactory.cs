namespace ENTech.Store.Infrastructure.Expandable
{
	public interface IDtoLoaderFactory
	{
		IDtoLoader<T> Create<T>();
	}
}