using System;

namespace ENTech.Store.Database.Exceptions
{
	public class EntityNotTrackedException : Exception
	{
		private const string ErrorMessage = "Entity state is not tracked.";

		public EntityNotTrackedException() : base(ErrorMessage)
		{
			
		}
	}
}