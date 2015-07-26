namespace ENTech.Store.Infrastructure.Mapping
{
	public interface IMapper
	{
		TResult Map<TSource, TResult>(TSource source);
	}
}