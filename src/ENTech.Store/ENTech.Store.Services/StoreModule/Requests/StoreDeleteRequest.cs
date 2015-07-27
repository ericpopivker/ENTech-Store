using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreDeleteRequest : IRequest<StoreDeleteResponse>
	{
		public int StoreId { get; set; }
	}
}