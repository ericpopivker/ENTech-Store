using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.External.ForCustomer.StoreModule.Dtos;

namespace ENTech.Store.Services.External.ForCustomer.StoreModule.Requests
{
	public class ProductFindRequest:ExternalRequestBase
	{
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		public ProductCriteriaDto Criteria { get; set; }
	}
}
