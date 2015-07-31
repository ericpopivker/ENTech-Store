using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Profiling;
using StackExchange.Redis.Extensions.Core;

namespace ENTech.Store.Infrastructure.Cache
{
	[Serializable]
	public class DictionaryCache : ICache
	{
		protected string CacheKeyPrepend
		{
			get { return "DictionaryCache_"; }
		}

		private ConcurrentDictionary<string, byte[]> _dictionary;
		private static ISerializer _serializer = new ProtobufSerializer();

		public DictionaryCache()
		{
			_dictionary = new ConcurrentDictionary<string, byte[]>();
		}

		#region ICache implementation

		public void Set<T>(string key, T value, CacheOpts opts = null) where T : class
		{
			using (MiniProfiler.Current.Step("DictionaryCache_Set_" + key))
			{
				var serializedValue = _serializer.Serialize(value);
				_dictionary.AddOrUpdate(key, serializedValue, (ky, val) => serializedValue);
			}
		}

		public void Set<T>(IList<Tuple<string, T>> tuples) where T : class
		{
			using (MiniProfiler.Current.Step("DictionaryCache_Set_" + tuples.Count + "_Tuples"))
			{
				foreach (var tuple in tuples)
					Set(tuple.Item1, tuple.Item2);
			}

		}

		public bool Exists(string key)
		{
			return _dictionary.ContainsKey(key);
		}

		public T Get<T>(string key) where T : class
		{
			using (MiniProfiler.Current.Step("DictionaryCache_Get_" + key))
			{
				byte[] serializedValue = _dictionary[key];
				T value = _serializer.Deserialize<T>(serializedValue);

				return value;
			}
		}

		public bool TryGet<T>(string key, ref T value) where T : class
		{
			using (MiniProfiler.Current.Step("DictionaryCache_TryGet_" + key))
			{
				byte[] serializedValue = null;
				var res = _dictionary.TryGetValue(key, out serializedValue);

				if (res)
					value = _serializer.Deserialize<T>(serializedValue);

				return res;
			}
		}


		public void Remove(string key)
		{
			byte[] value;
			if (!_dictionary.TryRemove(key, out value))
			{
				//potentially we can throw exception here but I think it's not necessary
			}
		}

		public bool TryRemove(string key)
		{
			byte[] value = null;
			return _dictionary.TryRemove(key, out value);
		}

		public void Remove(IEnumerable<string> keys)
		{
			foreach (string key in keys)
				TryRemove(key);
		}


		public void RemoveAll()
		{
			_dictionary.Clear();
		}

		public IDictionary<string, T> FindByKeys<T>(IEnumerable<string> keys) where T : class
		{
			using (MiniProfiler.StepStatic("DictionaryCache_FindByKeys_" + keys.Count() + "_Keys"))
			{
				var values = new Dictionary<string, T>();
				foreach (string key in keys)
				{
					T value = null;
					if (TryGet(key, ref value))
						values.Add(key, value);
					else
						values.Add(key, null);
				}
				return values;
			}
		}

		public IDictionary<string, T> FindByKeyPrefix<T>(string keyPrefix) where T : class
		{
			throw new NotImplementedException();
		}

		public System.Collections.Generic.IEnumerable<object> GetAll(string keyPrepend)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
