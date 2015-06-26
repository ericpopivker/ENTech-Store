using System.ComponentModel.DataAnnotations;
using ENTech.Store.Infrastructure.Services.Dtos;
using ENTech.Store.Infrastructure.Services.Validators;

namespace ENTech.Store.Services.SharedModule.Dtos
{
	public class FindCriteriaDtoBase<TSortField>
	{
		public PagingOptionsDto PagingOptions { get; set; }

		[Required]
		public SortingOptionsDto<TSortField> SortingOptions { get; set; }
	}
}
