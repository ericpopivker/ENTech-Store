using System;

namespace ENTech.Store.Infrastructure.Entities
{
	public interface IAuditable
	{
		DateTime CreatedAt { get; set; }

		DateTime LastUpdatedAt { get; set; }
	}
}