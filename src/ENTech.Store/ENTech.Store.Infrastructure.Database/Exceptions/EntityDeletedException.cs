using System;

namespace ENTech.Store.Infrastructure.Database.Exceptions
{
	public class EntityDeletedException : Exception
	{
		private const string ErrorMessage = "Entity has already been deleted";

		public EntityDeletedException()
			: base(ErrorMessage)
		{
			
		}

		public EntityDeletedException(Exception innerException)
			: base(ErrorMessage, innerException)
		{
			
		}
	}
}