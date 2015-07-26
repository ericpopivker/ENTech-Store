namespace ENTech.Store.Infrastructure.Mapping
{
	public class Mapper : IMapper
	{
		public TResult Map<TSource, TResult>(TSource source)
		{
			return AutoMapper.Mapper.Map<TResult>(source);
		}
	}
}