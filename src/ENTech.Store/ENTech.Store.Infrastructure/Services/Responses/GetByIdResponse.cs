namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class GetByIdResponse<TEntity> : InternalResponse
	{
		public TEntity Item { get; set; }
	}
}
