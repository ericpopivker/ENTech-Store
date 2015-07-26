using System;

namespace ENTech.Store.Infrastructure.Database.Exceptions
{
	public class EntityLoadMismatchException : Exception
	{
		private const string ErrorMessage = "Entities loaded mismatch expected result";

		public EntityLoadMismatchException()
			: base(ErrorMessage)
		{
			
		}

		public EntityLoadMismatchException(Exception innerException)
			: base(ErrorMessage, innerException)
		{
			
		}
	}
}