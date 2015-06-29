using ENTech.Store.Infrastructure.Services.Dtos;

namespace ENTech.Store.Infrastructure.Database.Repository
{
	public abstract class FindCriteriaBase
	{
		public PagingOptionsDto Paging { get; set; }
	}
}