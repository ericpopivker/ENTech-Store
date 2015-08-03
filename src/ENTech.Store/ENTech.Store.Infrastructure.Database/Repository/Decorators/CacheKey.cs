using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTech.Store.Infrastructure.Database.Repository.Decorators
{
	internal static class CacheKey
	{
		public static string ForEntityLastUpdatedAt<TEntity>(int entityId)
		{
			string key = String.Format("{0}_LastUpdatedAt", CacheKey.ForEntity<TEntity>(entityId));
			return key;
		}


		public static string ForEntity<TEntity>(int entityId)
		{
			string key = String.Format("{0}_{1}", typeof(TEntity).Name, entityId);
			return key;
		}
	}
}
