using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Profiling;
using MiniProfiler = StackExchange.Profiling.MiniProfiler;

namespace ENTech.Store.Infrastructure.Cache
{
	public class RedisCache : IDistributedCache
	{
		private static readonly Lazy<ConnectionMultiplexer> LazyConnection =
			new Lazy<ConnectionMultiplexer>(() =>
			{
				//See: http://stackoverflow.com/questions/25416562/stackexchange-redis-with-azure-redis-is-unusably-slow-or-throws-timeout-errors
				//http://stackoverflow.com/questions/18164893/this-command-is-not-available-unless-the-connection-is-created-with-admin-comman
				//AllowAdmin needed for FlushAll

				return ConnectionMultiplexer.Connect(ConfigurationOptions.Parse("localhost,abortConnect=false,allowAdmin=true"));
				//EnvSettings.GetConfigSettingStr("Redis_ConfigurationString")
			});


		private static ISerializer _serializer = new ProtobufSerializer();

		private ICacheClient GetClient()
		{
			var client = new StackExchangeRedisCacheClient(LazyConnection.Value, _serializer);
			return client;
		}

		private void TryOnClient(Action<ICacheClient> action)
		{
			var client = GetClient();
			//try
			//{
			action(client);
			//}
			//catch (Exception e)
			//{
			//	ErrorLogUtils.AddError(e);
			//}
			client = null;
		}

		private T TryOnClient<T>(Func<ICacheClient, T> action)
		{
			var client = GetClient();
			T result = default(T);
			//try
			//{
			result = action.Invoke(client);
			//}
			//catch (Exception e)
			//{
			//	ErrorLogUtils.AddError(e);
			//}
			client = null;
			return result;
		}

		public void Set<T>(string key, T value, CacheOpts opts = null) where T : class
		{
			using (MiniProfiler.Current.Step("RedisCache_Set_" + key))
			{
				EnsureCacheOpts(ref opts);
				TryOnClient(client =>
				{
					if (opts.IsSlidingExpireIn)
						client.Add(key, value, GetDefaultSlidingExpirationIfEmpty(opts.ExpireIn));
					else
					{
						if (opts.ExpireAt.HasValue)
							client.Add(key, value, GetDefaultExpirationDateIfEmpty(opts.ExpireAt));
						else
							client.Add(key, value);
					}

				});
			}
		}


		public void Set<T>(IList<Tuple<string, T>> tuples) where T : class
		{
			for (int i = 0; i < 3; i++)
			{
				if (i == tuples.Count)
					break;
				var tuple = tuples[i];
			}
			using (MiniProfiler.Current.Step("RedisCache_Set_" + tuples.Count + "_Tuples"))
			{
				TryOnClient(client =>
				{
					client.AddAll(tuples);
				});
			}
		}

		public bool Exists(string key)
		{
			using (MiniProfiler.StepStatic("RedisCache_Exists_" + key))
			{
				return TryOnClient(client => client.Exists(key));
			}
		}

		public T Get<T>(string key) where T : class
		{
			using (MiniProfiler.StepStatic("RedisCache_Get_" + key))
			{
				return TryOnClient(client =>
				{
					return client.Get<T>(key);
				}
				);
			}
		}

		public bool TryGet<T>(string key, ref T value) where T : class
		{
			using (MiniProfiler.StepStatic("RedisCache_TryGet_" + key))
			{
				T val = default(T);
				var result = TryOnClient((client) =>
				{
					val = client.Get<T>(key);
					if (val == null) return false;
					return true;
				});

				value = val;

				return result;
			}
		}

		public void Remove(string key)
		{
			using (MiniProfiler.StepStatic("RedisCache_Remove_" + key))
			{
				TryOnClient(client => client.Remove(key));
			}
		}

		public bool TryRemove(string key)
		{
			using (MiniProfiler.StepStatic("RedisCache_TryRemove_" + key))
			{
				return TryOnClient(client => client.Remove(key));
			}
		}

		public void Remove(IEnumerable<string> keys)
		{
			using (MiniProfiler.StepStatic("RedisCache_Remove_"  + keys.Count() + "_Keys"))
			{
				TryOnClient(client => client.RemoveAll(keys));
			}
		}

		public void RemoveAll()
		{
			using (MiniProfiler.StepStatic("RedisCache_RemoveAll"))
			{
				TryOnClient(client => client.FlushDb());
			}
		}


		public IDictionary<string, T> FindByKeys<T>(IEnumerable<string> keys) where T : class
		{
			using (MiniProfiler.StepStatic("RedisCache_FindByKeys_" + keys.Count() + "_Keys"))
			{
				return TryOnClient(client => client.GetAll<T>(keys));
			}
		}

		public IDictionary<string, T> FindByKeyPrefix<T>(string keyPrefix) where T : class
		{
			throw new NotImplementedException();
		}

		public T Get<T>(string key, out IConcurrencyHandle handle)
			where T : class, new()
		{
			using (MiniProfiler.StepStatic("RedisCache_GetConcurrent_" + key))
			{
				var token = this.GetType().ToString();
				var retryPolicy = new LockRetryPolicy();

				var result = TryOnClient(c =>
				{
					T item = default(T);
					retryPolicy.Handle(() =>
					{
						if (c.Database.LockTake(key, token, TimeSpan.FromSeconds(1)))
						{
							try
							{
								object obj = c.Get<object>(key);
								item = (T)obj;
							}
							finally
							{
								c.Database.LockRelease(key, token);
							}
						}
					});
					return item;
				});

				handle = new RedisLockHandle
				{
					Token = token
				};

				return result;
			}
		}

		public void Set<T>(string key, T value, IConcurrencyHandle handle) where T : class
		{
			using (MiniProfiler.StepStatic("RedisCache_SetConcurrent_" + key))
			{
				TryOnClient(c =>
				{
					if (c.Database.LockTake(key, ((RedisLockHandle)handle).Token, TimeSpan.FromSeconds(1)))
					{
						try
						{
							c.Add<object>(key, value, TimeSpan.FromHours(1));
						}
						finally
						{
							c.Database.LockRelease(key, ((RedisLockHandle)handle).Token);
						}
					}
				});
			}
		}

		public void Unlock(string key, IConcurrencyHandle handle)
		{
			using (MiniProfiler.StepStatic("RedisCache_Unlock_" + key))
			{
				var token = ((RedisLockHandle)handle).Token;
				TryOnClient(c => c.Database.LockRelease(key, token));
			}
		}

		private DateTime GetDefaultExpirationDateIfEmpty(DateTime? value)
		{
			if (value.HasValue)
				return value.Value;

			return DateTime.UtcNow.AddDays(1);
		}

		private TimeSpan GetDefaultSlidingExpirationIfEmpty(TimeSpan? value)
		{
			if (value.HasValue)
				return value.Value;

			return TimeSpan.FromDays(1);
		}

		private void EnsureCacheOpts(ref CacheOpts opts)
		{
			if (opts == null)
				opts = new CacheOpts();
		}

		private class RedisLockHandle : IConcurrencyHandle
		{
			public string Token { get; set; }
		}

	}
}