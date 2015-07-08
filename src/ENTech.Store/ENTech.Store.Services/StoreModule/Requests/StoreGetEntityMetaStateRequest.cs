using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Services.Misc;
using ENTech.Store.Services.SharedModule.Requests;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreGetEntityMetaStateRequest : SecureRequestBase<AnonymousSecurityInformation>
	{
		public int Id { get; set; }
	}
}