using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.Internal.PartnerModule.Requests
{
	public class PartnerVerifyAuthenticationRequest : IInternalRequest
	{
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required, AllowEmptyStrings = false)]
		public string PartnerToken { get; set; }
	}
}
