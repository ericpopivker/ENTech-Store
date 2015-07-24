using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.GeoModule.Requests
{
	public class AddressGetEntityMetaStateRequest :  SecureRequestBase<AnonymousSecurityInformation>
	{
		public int Id { get; set; }
	}
}