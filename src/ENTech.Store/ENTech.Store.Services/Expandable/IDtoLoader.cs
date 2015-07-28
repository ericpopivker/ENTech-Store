namespace ENTech.Store.Services.Expandable
{
	public interface IDtoLoader<T>
	{
		T Load(int id);
	}
}