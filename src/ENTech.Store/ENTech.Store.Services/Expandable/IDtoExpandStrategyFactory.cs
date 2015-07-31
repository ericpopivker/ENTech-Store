namespace ENTech.Store.Services.Expandable
{
	public interface IDtoExpandStrategyFactory
	{
		IDtoExpandStrategy<T> Create<T>(ExpandOption<T> expandOptions) where T : IExpandableDto;
	}
}