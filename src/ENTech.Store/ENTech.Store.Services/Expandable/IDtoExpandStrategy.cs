namespace ENTech.Store.Services.Expandable
{
	public interface IDtoExpandStrategy<T> where T:IExpandableDto
	{
		void Apply(T result);
	}
}