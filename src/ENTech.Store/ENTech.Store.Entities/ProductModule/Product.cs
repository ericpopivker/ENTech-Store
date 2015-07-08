using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.ProductModule
{
	[Table("Product")]
	public class Product : IEntity, IAuditable, ILogicallyDeletable
	{
		public int Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime LastUpdatedAt { get; set; }

		public DateTime? DeletedAt { get; set; }

		public bool IsDeleted { get; set; }


		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[Required]
		[MaxLength(20)]
		public string Sku { get; set; }

		[Required]
		public decimal Price { get; set; }

		public decimal TaxAmount { get; set; }

		public string Photo { get; set; }

		public bool IsActive { get; set; }

		[Required]
		public int? StoreId { get; set; }
		
		[ForeignKey("StoreId")]
		public virtual StoreModule.Store Store { get; set; }
		
	

	}
}