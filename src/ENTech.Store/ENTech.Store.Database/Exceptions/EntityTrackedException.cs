using System;

namespace ENTech.Store.Database.Exceptions
{
	public class EntityTrackedException : Exception
	{
		private const string ErrorMessage = "Entity state is already tracked.";

		public EntityTrackedException() : base(ErrorMessage)
		{
			
		}
	}
}