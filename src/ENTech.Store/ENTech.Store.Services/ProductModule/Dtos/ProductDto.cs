namespace ENTech.Store.Services.ProductModule.Dtos
{
	public class ProductDto
	{
		public int Id { get; set; }
		
		public int StoreId { get; set; }
		
		public string Name { get; set; }
		
		public decimal Price { get; set; }

		public string Sku { get; set; }
	}
}