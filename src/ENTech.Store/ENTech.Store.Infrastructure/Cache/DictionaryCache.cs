using System;
using System.Collections.Concurrent;

namespace ENTech.Store.Infrastructure.Cache
{
	[Serializable]
	public class DictionaryCache : ICache
	{
		protected string CacheKeyPrepend
		{
			get { return "DictionaryCache_"; }
		}

		private ConcurrentDictionary<string, object> _cacheData;

		public DictionaryCache()
		{
			_cacheData = new ConcurrentDictionary<string, object>();
		}

		#region ICache implementation

		public void Set(string key, object value, CacheOpts opts = null)
		{
			_cacheData.AddOrUpdate(key, value, (ky, val) => value);
		}

		public bool Exists(string key)
		{
			return _cacheData.ContainsKey(key);
		}

		public T Get<T>(string key)
		{
			//will throw exception if cannot find
			return (T)_cacheData[key];
		}

		public bool TryGet<T>(string key, ref T value)
		{
			object objValue = null;
			var res = _cacheData.TryGetValue(key, out objValue);
			if (res)
				value = (T)objValue;

			return res;
		}

		public void Remove(string key)
		{
			object value = null;
			if (!_cacheData.TryRemove(key, out value))
			{
				//potentially we can throw exception here but I think it's not necessary
			}
		}

		public bool TryRemove(string key)
		{
			object value = null;
			return _cacheData.TryRemove(key, out value);
		}

		//Merge shouldn't be used for Dictionary Cache
		public void Merge<T>(string key, T instance, CacheOpts opts = null)
		{
			throw new NotImplementedException();
		}

		//TryMerge shouldn't be used for Dictionary Cache
		public bool TryMerge<T>(string key, T instance, CacheOpts opts = null)
		{
			throw new NotImplementedException();
		}

		public void RemoveAll()
		{
			_cacheData.Clear();
		}

		public System.Collections.Generic.IEnumerable<object> GetAll(string keyPrepend)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
