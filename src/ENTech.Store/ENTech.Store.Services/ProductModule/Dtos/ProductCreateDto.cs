using System.ComponentModel.DataAnnotations;

namespace ENTech.Store.Services.ProductModule.Dtos
{
	public class ProductCreateDto
	{
		public int Id { get; set; }

		public int StoreId { get; set; }

		[Required]
		public string Name { get; set; }
	}
}