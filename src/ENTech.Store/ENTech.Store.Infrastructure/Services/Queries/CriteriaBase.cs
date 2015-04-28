namespace ENTech.Store.Infrastructure.Services.Queries
{
	public abstract class CriteriaBase
	{
		public int PageSize { get; set; }

		public int PageIndex { get; set; }

		public SortCriteria SortCriteria1 { get; set; }

		public SortCriteria SortCriteria2 { get; set; }

		protected CriteriaBase()
		{
			PageSize = 10;
			PageIndex = 1;
		}
	}
}
