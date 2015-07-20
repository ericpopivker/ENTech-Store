using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities.GeoModule
{
	[Table("State")]
	public class State : IDomainEntity
	{
		public int Id { get; set; }

		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(2)]
		public string Code { get; set; }
	}
}
