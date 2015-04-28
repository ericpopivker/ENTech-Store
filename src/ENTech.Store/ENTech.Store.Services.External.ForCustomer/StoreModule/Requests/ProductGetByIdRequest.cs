using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.External.ForCustomer.StoreModule.Requests
{
	public class ProductGetByIdRequest : ExternalRequestBase
	{
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		[Range(1, int.MaxValue, ErrorMessage = RequestValidatorErrorMessage.PositiveInt)]
		public int ProductId { get; set; }
	}
}
