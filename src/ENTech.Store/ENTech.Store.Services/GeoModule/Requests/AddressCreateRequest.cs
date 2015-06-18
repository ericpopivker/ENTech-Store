using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.GeoModule.Requests
{
	public class AddressCreateRequest : SecureRequestBase<AnonymousSecurityInformation>
	{
		public AddressCreateDto Address { get; set; }
	}
}