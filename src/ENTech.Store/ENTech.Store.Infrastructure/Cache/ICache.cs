﻿using System;
using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Cache
{
	public interface ICache
	{
		void Set<T>(string key, T value, CacheOpts opts = null) where T : class;

		void Set<T>(IList<Tuple<string, T>> tuples) where T : class;

		bool Exists(string key);

		T Get<T>(string key) where T : class;

		bool TryGet<T>(string key, ref T value) where T : class;

		void Remove(string key);

		bool TryRemove(string key);

		void Remove(IEnumerable<string> keys);

		void RemoveAll();

		IDictionary<string, T> FindByKeys<T>(IEnumerable<string> keys) where T : class;

		IDictionary<string, T> FindByKeyPrefix<T>(string keyPrefix) where T : class;
	}
}
