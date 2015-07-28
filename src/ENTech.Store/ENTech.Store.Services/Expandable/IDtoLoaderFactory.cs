namespace ENTech.Store.Services.Expandable
{
	public interface IDtoLoaderFactory
	{
		IDtoLoader<T> Create<T>();
	}
}