using System;

namespace ENTech.Store.Infrastructure.Database.Repository.Exceptions
{
	public class NonPersistentEntityException<T> : Exception
	{
		private static string GetFormattedMessage()
		{
			return string.Format("Entity of type \"{0}\" is not supported by the database implementation", typeof(T).FullName);
		}

		public NonPersistentEntityException()
			: base(GetFormattedMessage())
		{
			
		}

		public NonPersistentEntityException(Exception innerException)
			: base(GetFormattedMessage(), innerException)
		{
			
		}
	}
}