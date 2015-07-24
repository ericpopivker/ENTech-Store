using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.SharedModule.Dtos
{
	public class SortFieldDto
	{
		[Required]
		public string SortField { get; set; }

		[Required]
		public bool? IsDescending { get; set; }
	}
}
