using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Dtos
{
	public class SortingOptionsDto<TSortColumnEnum>
	{
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		public TSortColumnEnum Column { get; set; }

		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		public SortingDirectionDto Direction { get; set; }
	}
}