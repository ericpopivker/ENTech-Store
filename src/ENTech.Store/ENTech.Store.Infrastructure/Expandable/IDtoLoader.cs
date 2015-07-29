namespace ENTech.Store.Infrastructure.Expandable
{
	public interface IDtoLoader<T>
	{
		T Load(int id);
	}
}