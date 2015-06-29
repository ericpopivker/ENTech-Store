using ENTech.Store.Services.SharedModule.Dtos;

namespace ENTech.Store.Services.StoreModule.Dtos
{
	public class StoreFindCriteriaDto : FindCriteriaDtoBase<StoreSortField>
	{
		public string Name { get; set; }
	}
}