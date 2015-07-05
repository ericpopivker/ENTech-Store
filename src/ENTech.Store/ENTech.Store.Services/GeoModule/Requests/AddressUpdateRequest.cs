using ENTech.Store.Services.GeoModule.Dtos;
using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.GeoModule.Requests
{
	public class AddressUpdateRequest : SecureRequestBase<AnonymousSecurityInformation>
	{
		public int AddressId { get; set; }
		public AddressCreateOrUpdateDto Address { get; set; }
	}
}