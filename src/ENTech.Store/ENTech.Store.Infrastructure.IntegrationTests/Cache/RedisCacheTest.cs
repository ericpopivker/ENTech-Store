using ENTech.Store.Infrastructure.Cache;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.IntegrationTests.Cache
{
	[TestFixture]
	public class RedisCacheTest : CacheTestBase
	{
		public RedisCacheTest() : base(new RedisCache())
		{

		}
	}
}

