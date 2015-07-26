namespace ENTech.Store.Infrastructure.Database.QueryExecuter
{
	public class PagingOptions
	{
		public int PageSize { get; set; }

		public int PageIndex { get; set; }

		public PagingOptions()
		{
			PageIndex = 1;
			PageSize = 10;
		}
	}
}