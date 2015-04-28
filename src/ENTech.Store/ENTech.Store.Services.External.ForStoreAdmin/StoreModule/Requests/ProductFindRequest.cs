using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;
using ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Dtos;

namespace ENTech.Store.Services.External.ForStoreAdmin.StoreModule.Requests
{
	public class ProductFindRequest:ExternalRequestBase
	{
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		public ProductCriteriaDto Criteria { get; set; }
	}
}
