using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.GeoModule.Requests
{
	public class AddressDeleteRequest : SecureRequestBase<AnonymousSecurityInformation>
	{
		public int AddressId { get; set; }
	}
}