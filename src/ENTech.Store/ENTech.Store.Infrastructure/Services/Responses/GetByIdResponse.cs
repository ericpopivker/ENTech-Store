namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class GetByIdResponse<TEntity> : ResponseBase
	{
		public TEntity Item { get; set; }
	}
}
