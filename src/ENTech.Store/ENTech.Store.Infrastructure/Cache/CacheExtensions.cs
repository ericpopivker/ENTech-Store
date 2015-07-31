using System;

namespace ENTech.Store.Infrastructure.Cache
{
	public static class CacheExtensions
	{
		public static T GetOrSet<T>(this ICache cache, string key, Func<T> setter, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null) where T : class
		{
			object cached = null;
			if (cache.TryGet(key, ref cached))
			{
				if (cached is T)
				{
					return (T)cached;
				}
			}
			var value = setter();

			var cacheOpts = new CacheOpts()
			{
				ExpireIn = slidingExpiration ?? absoluteExpiration,
				IsSlidingExpireIn = slidingExpiration.HasValue
			};

			cache.Set(key, value, cacheOpts);

			return value;
		}
	}
}
