namespace ENTech.Store.Services.Expandable
{
	public interface IDtoExpandStrategy<T> 
	{
		void Apply(T result);
	}
}