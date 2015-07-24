namespace ENTech.Store.Services.ProductModule.Commands
{
	public interface IProductQuery
	{
		int GetTotalByStoreId(int storeId);

		bool ExistsByName(string name, int storeId);
	}
}