using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.External.ForPartner.PartnerModule.Requests
{
	public class PartnerAuthenticateRequest : ExternalRequestBase
	{
		//hide
		internal string UserToken { get; private set; }

		//hide
		internal new string PartnerToken { get; private set; }

		//hide
		internal new int StoreId { get; private set; }

		[Required(ErrorMessage = RequestValidatorErrorMessage.Required, AllowEmptyStrings = false)]
		public string Key { get; set; }

		[Required(ErrorMessage = RequestValidatorErrorMessage.Required, AllowEmptyStrings = false)]
		public string Secret { get; set; }
	}
}