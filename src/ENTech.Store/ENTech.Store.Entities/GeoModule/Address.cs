using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.GeoModule
{
	[Table("Address")]
	public class Address : IEntity
	{
		public int Id { get; set; }

		[MaxLength(100)]
		[Required]
		public string Street { get; set; }
		
		[MaxLength(100)]
		public string Street2 { get; set; }
		
		[MaxLength(100)]
		[Required]
		public string City { get; set; }

		public int? StateId { get; set; }

		[ForeignKey("StateId")]
		public virtual State State { get; set; }


		[MaxLength(20)]
		public string Zip { get; set; }
	}
}
