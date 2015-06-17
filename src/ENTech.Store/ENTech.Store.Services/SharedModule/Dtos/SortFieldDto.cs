using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.SharedModule.Dtos
{
	public class SortFieldDto
	{
		public string SortField { get; set; }

		public bool? IsDescending { get; set; }
	}
}
