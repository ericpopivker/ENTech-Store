namespace ENTech.Store.Services.Expandable
{
	public interface IDtoExpander
	{
		T LoadAndExpand<T>(int entityId) where T : IExpandableDto;
	}

	public interface IDtoLoaderFactory
	{
		IDtoLoader<T> Create<T>();
	}

	public interface IDtoLoader<T>
	{
		T Load(int id);
	}
}