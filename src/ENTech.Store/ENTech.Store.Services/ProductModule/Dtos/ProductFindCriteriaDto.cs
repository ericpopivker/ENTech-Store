using ENTech.Store.Services.SharedModule.Dtos;

namespace ENTech.Store.Services.ProductModule.Dtos
{
	public class ProductFindCriteriaDto : FindCriteriaDtoBase<ProductSortField>
	{
		public string Name { get; set; }
	}
}