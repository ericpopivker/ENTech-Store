using System;
using System.CodeDom;
using System.Collections.Generic;

namespace ENTech.Store.Infrastructure.Cache
{
	public interface IDistributedCache : ICache
	{
		T Get<T>(string key, out IConcurrencyHandle handle) where T : class, new();

		void Set<T>(string key, T value, IConcurrencyHandle handle) where T : class;
		
		void Unlock(string key, IConcurrencyHandle handle);

	}
}