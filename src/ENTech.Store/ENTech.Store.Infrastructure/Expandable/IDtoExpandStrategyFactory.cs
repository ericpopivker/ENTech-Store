namespace ENTech.Store.Infrastructure.Expandable
{
	public interface IDtoExpandStrategyFactory
	{
		IDtoExpandStrategy<T> Create<T>(ExpandOption<T> expandOptions);
	}
}