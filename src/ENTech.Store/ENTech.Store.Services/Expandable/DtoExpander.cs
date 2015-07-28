namespace ENTech.Store.Services.Expandable
{
	public class DtoExpander : IDtoExpander
	{
		private readonly IDtoLoaderFactory _dtoLoaderFactory;

		public DtoExpander(IDtoLoaderFactory dtoLoaderFactory)
		{
			_dtoLoaderFactory = dtoLoaderFactory;
		}

		public T LoadAndExpand<T>(int entityId) where T : IExpandableDto
		{
			var loader = _dtoLoaderFactory.Create<T>();
			var result = loader.Load(entityId);
			return result;
		}
	}
}