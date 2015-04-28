using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.External.ForStoreAdmin.CustomerModule.Dtos
{
	public class SortFieldDto
	{
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		public string SortField { get; set; }

		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		public bool? IsDescending { get; set; }
	}
}
