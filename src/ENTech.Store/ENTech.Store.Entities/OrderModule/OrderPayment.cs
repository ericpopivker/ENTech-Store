using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.OrderModule
{
	[Table("OrderPayment")]
	public class OrderPayment : IDomainEntity
	{
		public int Id { get; set; }

		public decimal TotalAmount { get; set; }

	}
}