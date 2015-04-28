using System;

namespace ENTech.Store.Infrastructure.Entities
{
	public interface ILogicallyDeletable
	{
		bool IsDeleted { get; set; } 
		
		DateTime? DeletedAt { get; set; }
	}
}