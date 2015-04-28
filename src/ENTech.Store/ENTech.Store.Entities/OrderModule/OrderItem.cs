using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.OrderModule
{
	[Table("OrderItem")]
	public class OrderItem : IEntity
	{
		public int Id { get; set; }

		[Required]
		public int? Quantity { get; set; }

		[Required]
		public decimal? UnitPrice { get; set; }

		[Required]
		public decimal? SubTotal { get; set; }


		[Required]
		public int? OrderId { get; set; }
		
		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; }


		[Required]
		public int? ProductId { get; set; }
		
		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
}
}