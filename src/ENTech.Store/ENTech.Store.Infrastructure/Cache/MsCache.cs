using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace ENTech.Store.Infrastructure.Cache
{
	public class MsCache : ICache
	{
		//private static readonly MsCache _instance = null;
		//public static MsCache Instance
		//{
		//	get { return _instance; }
		//}

		public CacheOpts DefaultOpts { get; set; }


		//static MsCache()
		//{
		//	_instance = new MsCache();
		//}

		public MsCache()
		{
			DefaultOpts = new CacheOpts();
		}

		public IEnumerable<object> GetAll(string keyPrepend)
		{
			return MemoryCache.Default.Where(x => x.Key.StartsWith(keyPrepend)).Select(x => x.Value);
		}

		public void Set(string key, object value, CacheOpts opts = null)
		{
			if (opts == null)
				opts = DefaultOpts;

			//ASP.NET Cache doesn't allow nulls so use DBNull instead
			if (value == null)
				value = DBNull.Value;

			if (opts.ExpireAt.HasValue)
			{
				MemoryCache.Default.Add(key, value, opts.ExpireAt.Value);
			}
			else if (opts.ExpireIn.HasValue)
			{
				if (opts.IsSlidingExpireIn)
				{
					MemoryCache.Default.Add(key, value, new CacheItemPolicy { SlidingExpiration = opts.ExpireIn.Value });
				}
				else
				{
					DateTimeOffset expireAt = DateTimeOffset.UtcNow.Add(opts.ExpireIn.Value);
					MemoryCache.Default.Add(key, value, new CacheItemPolicy { AbsoluteExpiration = expireAt });
				}
			}
			else
			{
				MemoryCache.Default.Add(key, value, null);
			}
		}
		
		public T GetOrSet<T>(string key, Func<T> setter, TimeSpan? slidingExpiration = null, TimeSpan? absoluteExpiration = null)
		{
			return CacheExtensions.GetOrSet(this, key, setter, slidingExpiration, absoluteExpiration);
		}

		public bool Exists(string key)
		{
			object value = null;
			return TryGet(key, ref value);
		}

		public T Get<T>(string key)
		{
			object obj = MemoryCache.Default.Get(key);

			if (obj == null)
				throw new InvalidOperationException("Key " + key + " is not in cache.");

			return (T)obj;
		}

		public bool TryGet<T>(string key, ref T value)
		{
			object obj = MemoryCache.Default.Get(key);

			if (obj == null)        //Doesn't exist
				return false;

			//No need to handle sliding expiration since it works
			if (obj != DBNull.Value)
				value = (T)obj;

			return true;
		}

		public void Remove(string key)
		{
			MemoryCache.Default.Remove(key);
		}

		public bool TryRemove(string key)
		{
			object o = MemoryCache.Default.Remove(key);

			if (o == null)
				return false;

			return true;
		}

		public void RemoveAll()
		{
			foreach (var element in MemoryCache.Default)
				MemoryCache.Default.Remove(element.Key);
		}

		public long Count()
		{
			return MemoryCache.Default.GetCount();
		}
	}
}
