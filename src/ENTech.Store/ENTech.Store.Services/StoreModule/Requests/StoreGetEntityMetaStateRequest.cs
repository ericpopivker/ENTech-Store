using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.StoreModule.Responses;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreGetEntityMetaStateRequest : IRequest<StoreGetEntityMetaStateResponse>
	{
		public int Id { get; set; }
	}
}