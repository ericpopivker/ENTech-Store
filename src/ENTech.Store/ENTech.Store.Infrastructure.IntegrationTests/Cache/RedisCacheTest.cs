using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Services;
using NUnit.Framework;
using ProtoBuf;

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

