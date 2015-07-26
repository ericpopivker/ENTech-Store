using ENTech.Store.Infrastructure.Services.Requests;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreDeleteRequest : IRequest
	{
		public int StoreId { get; set; }
	}
}