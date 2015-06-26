namespace ENTech.Store.Services.ProductModule.Commands
{
	public interface IProductQuery
	{
		int GetTotalByStoreId(int storeId);
	}
}