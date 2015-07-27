using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.GeoModule.Responses;

namespace ENTech.Store.Services.GeoModule.Requests
{
	public class AddressDeleteRequest : IRequest<AddressDeleteResponse>
	{
		public int AddressId { get; set; }
	}
}