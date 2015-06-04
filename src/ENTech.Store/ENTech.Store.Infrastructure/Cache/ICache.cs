using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Cache
{
	public interface ICache
	{
		void Set(string key, object value, CacheOpts opts = null);

		bool Exists(string key);

		T Get<T>(string key);

		bool TryGet<T>(string key, ref T value);

		void Remove(string key);

		bool TryRemove(string key);
		
		void RemoveAll();

		IEnumerable<object> GetAll(string keyPrepend);
	}
	

	public interface IDistributedCache : ICache
	{
		T Get<T>(string key, out IConcurrencyHandle handle) where T : new();
		void Set<T>(string key, T value, IConcurrencyHandle handle);
		void Unlock(string key, IConcurrencyHandle handle);
	}

	public interface IConcurrencyHandle
	{
	}
}
