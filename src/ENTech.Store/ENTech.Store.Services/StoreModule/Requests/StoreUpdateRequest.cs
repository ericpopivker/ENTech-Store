using System.ComponentModel.DataAnnotations;
using ENTech.Store.Services.Misc;
using ENTech.Store.Services.StoreModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Requests
{
	public class StoreUpdateRequest : SecureRequestBase<BusinessAdminSecurityInformation>
	{
		[Required]
		public StoreUpdateDto Store { get; set; }
		public int StoreId { get; set; }
	}
}