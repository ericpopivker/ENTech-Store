using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.External.ForCustomer.StoreModule.Dtos
{
	public class ProductCriteriaDto
	{
		[Range(1, int.MaxValue, ErrorMessage = RequestValidatorErrorMessage.PositiveInt)]
		public int PageSize { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = RequestValidatorErrorMessage.PositiveInt)]
		public int PageIndex { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = RequestValidatorErrorMessage.PositiveInt)]
		public int? CategoryId { get; set; }

		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		public SortFieldDto SortField1 { get; set; }

		public SortFieldDto SortField2 { get; set; }
	}
}
