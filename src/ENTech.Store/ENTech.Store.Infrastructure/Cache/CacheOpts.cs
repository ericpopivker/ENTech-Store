using System;

namespace ENTech.Store.Infrastructure.Cache
{
	public class CacheOpts
	{
		public CacheOpts()
		{
			ExpireAt = null;
			ExpireIn = null;
			IsSlidingExpireIn = true;
		}

		public DateTime? ExpireAt { get; set; }
		public TimeSpan? ExpireIn { get; set; }
		public bool IsSlidingExpireIn { get; set; }
	}
}
