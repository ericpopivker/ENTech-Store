namespace ENTech.Store.Infrastructure.Services.Responses
{
	public class GetByIdResponse<TEntity> : Response
	{
		public TEntity Item { get; set; }
	}
}
