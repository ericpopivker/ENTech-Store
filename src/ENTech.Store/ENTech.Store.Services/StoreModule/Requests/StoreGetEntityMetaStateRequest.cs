using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreGetEntityMetaStateRequest : SecureRequestBase<AnonymousSecurityInformation>
	{
		public int Id { get; set; }
	}
}