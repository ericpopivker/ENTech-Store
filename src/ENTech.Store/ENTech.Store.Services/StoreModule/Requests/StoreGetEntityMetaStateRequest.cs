using ENTech.Store.Infrastructure.Services.Requests;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreGetEntityMetaStateRequest : IRequest
	{
		public int Id { get; set; }
	}
}