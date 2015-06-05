namespace ENTech.Store.Infrastructure.Services.Dtos
{
	public class PagingOptionsDto
	{
		public int PageSize { get; set; }

		public int PageIndex { get; set; }

		public PagingOptionsDto()
		{
			PageIndex = 1;
			PageSize = 10;
		}
	}
}