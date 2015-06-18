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
		[Required]
		public string City { get; set; }

		[MaxLength(20)]
		public string Zip { get; set; }
		
		[MaxLength(100)]
		public string Street2 { get; set; }

		[ForeignKey("StateId")]
		public virtual State State { get; set; }

		public string StateOther { get; set; }

		[ForeignKey("CountryId")]
		public virtual Country Country { get; set; }
	}
}
