using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Requests;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.External
{
	public abstract class ExternalRequestBase : IExternalRequest
	{
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		[Range(1, int.MaxValue, ErrorMessage = RequestValidatorErrorMessage.PositiveInt)]
		public virtual int StoreId { get; set; }

		public virtual string PartnerToken { get; set; }
	}

}
