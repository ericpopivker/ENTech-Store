using ENTech.Store.Infrastructure.Services.Dtos;

namespace ENTech.Store.Infrastructure.Services.Requests
{
	public class ListReportRequest<TColumnEnum, TFilterColumnEnum, TSortColumnEnum> : ListReportRequestBase<TColumnEnum, TFilterColumnEnum, TSortColumnEnum>
	{
		public PagingOptionsDto PagingOptions { get; set; }
	}
}