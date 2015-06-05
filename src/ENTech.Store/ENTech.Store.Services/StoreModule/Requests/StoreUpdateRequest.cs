using ENTech.Store.Services.Misc;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreUpdateRequest : SecureRequestBase<BusinessAdminSecurityInformation>
	{
		public StoreUpdateDto Store { get; set; }
	}
}