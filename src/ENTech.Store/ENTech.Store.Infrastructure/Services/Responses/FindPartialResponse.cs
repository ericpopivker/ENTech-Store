namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class FindPartialResponse<TEntity> : FindResponse<TEntity>
	{
		public int TotalCount { get; set; }
	}
}
