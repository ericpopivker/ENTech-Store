using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Infrastructure.Services.Dtos
{
	public class SortingOptionsDto<TSortColumnEnum>
	{
		public TSortColumnEnum Column { get; set; }

		public SortingDirectionDto Direction { get; set; }
	}
}