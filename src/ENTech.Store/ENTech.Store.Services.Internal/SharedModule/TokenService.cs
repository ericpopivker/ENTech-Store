using System;
using ENTech.Store.Infrastructure.Cache;

namespace ENTech.Store.Services.Internal.SharedModule
{
	public class TokenService : ITokenService
	{
		private readonly ICache _cache;

		public TokenService(ICache cache)
		{
			_cache = cache;
		}

		public TokenData Create(object associatedObject)
		{
			var token = Guid.NewGuid().ToString();
			var expireAt = DateTime.UtcNow;
			expireAt = expireAt.AddDays(2);

			var opts = new CacheOpts() { ExpireAt = expireAt, IsSlidingExpireIn = false };

			_cache.Set(GetCacheKey(token), associatedObject, opts);

			var data = new TokenData();

			data.Token = token;
			data.ExpireAt = expireAt;
			return data;
		}

		public T GetByToken<T>(string token)
		{
			T cacheObject = default(T);
			if (_cache.TryGet(token, ref cacheObject))
			{
				return cacheObject;
			}
			return default(T);
		}

		private string GetCacheKey(string token)
		{
			return String.Format("ENTech_Store_Token_{0}", token);
		}
	}

	public class TokenData
	{
		public string Token { get; set; }

		public DateTime ExpireAt { get; set; }
	}
}
