namespace ENTech.Store.Infrastructure.Expandable
{
	public interface IDtoExpandStrategy<T> 
	{
		void Apply(T result);
	}
}