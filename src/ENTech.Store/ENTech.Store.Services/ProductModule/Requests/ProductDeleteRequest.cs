using ENTech.Store.Services.Misc;

namespace ENTech.Store.Services.ProductModule.Requests
{
	public class ProductDeleteRequest : SecureRequestBase<BusinessAdminSecurityInformation>
	{
		public int Id { get; set; }
	}
}