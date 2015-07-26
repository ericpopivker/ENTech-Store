using System;

namespace ENTech.Store.Infrastructure.Database.EF6.Utility
{
	public class EntityNotTrackedException : Exception
	{
		private const string ErrorMessage = "Entity state is not tracked.";

		public EntityNotTrackedException() : base(ErrorMessage)
		{
			
		}
	}
}