using System.Collections.Generic;
using ENTech.Store.Infrastructure.Services.Dtos;
using ENTech.Store.Infrastructure.Services.Filters;

namespace ENTech.Store.Infrastructure.Services.Requests
{
	public class ListReportRequestBase<TColumnEnum, TFilterColumnEnum, TSortColumnEnum>
	{
		public IEnumerable<TColumnEnum> Columns { get; set; }
		public IList<ListReportFilter<TFilterColumnEnum>> Filters { get; set; }
		public SortingOptionsDto<TSortColumnEnum> SortingOptions { get; set; }
		public int BusinessId { get; set; }
	}
}