namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class GetByIdResponse<TEntity> : IResponse
	{
		public TEntity Item { get; set; }
	}
}
