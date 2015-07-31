using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Cache;
using MiniProfiler.Windows;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.IntegrationTests.Cache
{
	[TestFixture]
	public class RedisCachePerformanceTest : CachePerformanceTestBase
	{

		public RedisCachePerformanceTest() : base(new RedisCache())
		{
		}
	}
}
