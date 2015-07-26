using System;

namespace ENTech.Store.Infrastructure.Database.Exceptions
{
	public class EntityPersistenceException : Exception
	{
		private const string ErrorMessage = "Entity is not attached to the database";

		public EntityPersistenceException()
			: base(ErrorMessage)
		{
			
		}

		public EntityPersistenceException(Exception innerException)
			: base(ErrorMessage, innerException)
		{
			
		}
	}
}