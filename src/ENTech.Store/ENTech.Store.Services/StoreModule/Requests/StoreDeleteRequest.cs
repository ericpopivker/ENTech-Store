using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreDeleteRequest : SecureRequestBase<AnonymousSecurityInformation>
	{
		public int StoreId { get; set; }
	}
}