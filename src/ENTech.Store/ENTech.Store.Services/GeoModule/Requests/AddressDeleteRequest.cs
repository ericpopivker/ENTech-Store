using ENTech.Store.Infrastructure.Services.Requests;

namespace ENTech.Store.Services.GeoModule.Requests
{
	public class AddressDeleteRequest : IRequest
	{
		public int AddressId { get; set; }
	}
}