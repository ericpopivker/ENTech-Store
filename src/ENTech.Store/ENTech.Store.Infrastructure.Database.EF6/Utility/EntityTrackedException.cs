using System;

namespace ENTech.Store.Infrastructure.Database.EF6.Utility
{
	public class EntityTrackedException : Exception
	{
		private const string ErrorMessage = "Entity state is already tracked.";

		public EntityTrackedException() : base(ErrorMessage)
		{
			
		}
	}
}