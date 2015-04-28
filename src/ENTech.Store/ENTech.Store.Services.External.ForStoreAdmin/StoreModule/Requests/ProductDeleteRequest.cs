using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Requests
{
	public class ProductDeleteRequest : ExternalRequestBase
	{
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		[Range(1, int.MaxValue, ErrorMessage = RequestValidatorErrorMessage.PositiveInt)]
		public int ProductId { get; set; }
	}
}
