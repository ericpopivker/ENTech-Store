using ENTech.Store.Services.Misc;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreCreateRequest : SecureRequestBase<AnonymousSecurityInformation>
	{
		public StoreCreateDto Store { get; set; }
	}
}