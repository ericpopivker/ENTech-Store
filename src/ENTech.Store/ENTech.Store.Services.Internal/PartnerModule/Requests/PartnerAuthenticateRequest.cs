using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.Internal.PartnerModule.Requests
{
	public class PartnerAuthenticateRequest : IInternalRequest
	{
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required, AllowEmptyStrings = false)]
		public string Key { get; set; }

		[Required(ErrorMessage = RequestValidatorErrorMessage.Required, AllowEmptyStrings = false)]
		public string Secret { get; set; }
	}
}
