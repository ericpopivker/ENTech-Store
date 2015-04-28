using System;

namespace ENTech.Store.Infrastructure.Cache
{
	public class AzureCache : IConcurrencyCache
	{
		protected string CacheKeyPrepend
		{
			get { return "AppFabricCache_"; }
		}

		private static AzureCache _instance = null;
		private static object _lockObject = new object();
		private Guid? _lastCacheVersion;
		public static AzureCache Instance
		{
			get { return _instance; }
		}

		private DataCache _dataCache = default(DataCache);

		public CacheOpts DefaultOpts { get; set; }

		static AzureCache()
		{
			_instance = new AzureCache();
		}

		private AzureCache()
		{
			InitDataCache();
		}

		protected void InitDataCache()
		{
			var currentLastVersion = _lastCacheVersion;
			lock (_lockObject)
			{
				if (_lastCacheVersion == currentLastVersion)
				{
					_dataCache = GetDataCache(EnvironmentUtils.GetConfigSettingStr("AzureCache_CacheName"));
					_lastCacheVersion = Guid.NewGuid();
					DefaultOpts = new CacheOpts();
				}
			}
		}

		private DataCache GetDataCache(string name)
		{
			DataCache cache = null;

			HandleBadDataCacheBug(() =>
				{
					var factorySecurity = new DataCacheSecurity(EnvironmentUtils.GetConfigSettingStr("AzureCache_Token"));

					var factoryConfig = new DataCacheFactoryConfiguration();
					factoryConfig.SecurityProperties = factorySecurity;
					factoryConfig.AutoDiscoverProperty = new DataCacheAutoDiscoverProperty(true,
					                                                                       EnvironmentUtils.GetConfigSettingStr(
						                                                                       "AzureCache_Endpoint"));

					factoryConfig.UseLegacyProtocol = false;
					factoryConfig.MaxConnectionsToServer = 1;
					factoryConfig.ChannelOpenTimeout = TimeSpan.FromSeconds(3);
					factoryConfig.RequestTimeout = TimeSpan.FromSeconds(3);

					factoryConfig.IsCompressionEnabled = true;

					var dataCacheFactory = new DataCacheFactory(factoryConfig);

					cache = dataCacheFactory.GetCache(name);
				});

			return cache;
		}

		public void Set(string key, object value, CacheOpts opts = null)
		{
			if (opts == null)
				opts = DefaultOpts;

			if (value == null)
				value = DBNull.Value;

			HandleBadDataCacheBug(() =>
				{
					if (opts.ExpireAt.HasValue)
					{
						_dataCache.Add(key, value, opts.ExpireAt.Value - DateTime.Now);
					}
					else if (opts.ExpireIn.HasValue)
					{
						_dataCache.Add(key, value, opts.ExpireIn.Value);
					}
					else
					{
						_dataCache.Add(key, value);
					}
				});
		}

		public bool Exists(string key)
		{
			object value = null;
			return TryGet(key, ref value);
		}

		public T Get<T>(string key)
		{
			object obj = null;
			HandleBadDataCacheBug(() =>
				{
					obj = _dataCache.Get(key);
				});

			if (obj == null)
				throw new InvalidOperationException("Key " + key + " is not in cache.");

			return (T)obj;
		}

		public bool TryGet<T>(string key, ref T value)
		{
			object obj = null;
			HandleBadDataCacheBug(() => {
				                            obj = _dataCache.Get(key);
			});

			if (obj == null)        //Doesn't exist
				return false;

			//No need to handle sliding expiration since it works
			if (obj != DBNull.Value)
				value = (T)obj;

			return true;
		}

		public void Remove(string key)
		{
			TryRemove(key);
		}

		public bool TryRemove(string key)
		{
			var result = false;
			HandleBadDataCacheBug(() =>
				{
					result = _dataCache.Remove(key);
				});
			return result;
		}

		public void RemoveAll()
		{
			_dataCache.Clear();
		}

		public System.Collections.Generic.IEnumerable<object> GetAll(string keyPrepend)
		{
			throw new NotImplementedException();
		}

		public T Get<T>(string key, out IConcurrencyHandle handle) where T : new()
		{
			var retryPolicy = new LockRetryPolicy();
			DataCacheLockHandle lockHandle = null;
			object item = null;

			HandleBadDataCacheBug(() => retryPolicy.Handle(() =>
				{
					item = _dataCache.GetAndLock(key, TimeSpan.FromSeconds(1), out lockHandle, true);
				}));

			handle = new AzureLockHandle
				{
					Instance = lockHandle
				};
			return (T) item;
		}

		public void Unlock(string key, IConcurrencyHandle handle)
		{
			var azureLockHandle = (AzureLockHandle)handle;
			HandleBadDataCacheBug(() =>
				{
					_dataCache.Unlock(key, azureLockHandle.Instance, TimeSpan.FromHours(1));
				});
		}

		public void Set<T>(string key, T value, IConcurrencyHandle handle)
		{
			var azureLockHandle = (AzureLockHandle) handle;
			HandleBadDataCacheBug(() =>
				{
					_dataCache.PutAndUnlock(key, value, azureLockHandle.Instance, TimeSpan.FromHours(1));
				});
		}

		private void HandleBadDataCacheBug(Action action)
		{
			var retryStrategy = new Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

			var retryPolicy =
			  new RetryPolicy<CacheRetryPolicy>(retryStrategy);
			try
			{
				retryPolicy.ExecuteAction(action);
			}
			catch (DataCacheException ex)
			{
				if (ex.ErrorCode == DataCacheErrorCode.ConnectionTerminated || ex.ErrorCode == DataCacheErrorCode.Timeout ||
				    ex.ErrorCode == DataCacheErrorCode.RetryLater)
				{
					ErrorLogUtils.AddError(
						new Exception(
							"DataCache connection was lost. Trying to reinitialize DataCache through DataCacheFactory and retrying last operation.",
							ex));
					InitDataCache();
					retryPolicy.ExecuteAction(action);
				}
				else
				{
					throw;
				}
			}
		}
		
		private class AzureLockHandle : IConcurrencyHandle
		{
			public DataCacheLockHandle Instance { get; set; }
		}

		private class CacheRetryPolicy : ITransientErrorDetectionStrategy
		{
			public bool IsTransient(Exception ex)
			{
				return ex.GetType() == typeof (DataCacheException);
			}
		}
	}
}
