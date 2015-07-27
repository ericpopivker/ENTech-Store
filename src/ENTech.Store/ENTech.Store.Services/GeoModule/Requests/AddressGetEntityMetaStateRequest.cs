using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Requests
{
	public class AddressGetEntityMetaStateRequest : IRequest<AddressGetEntityMetaStateResponse>
	{
		public int Id { get; set; }
	}
}