using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.SharedModule.Dtos
{
	public class SortFieldDto
	{
		/// <summary>
		/// Possible values: see enum <see cref="EventModule.Queries.EventCriteria.SortField"/>
		/// </summary>
		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		public string SortField { get; set; }

		[Required(ErrorMessage = RequestValidatorErrorMessage.Required)]
		public bool? IsDescending { get; set; }
	}
}
