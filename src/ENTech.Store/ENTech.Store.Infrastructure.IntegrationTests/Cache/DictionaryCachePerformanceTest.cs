using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Cache;
using MiniProfiler.Windows;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.IntegrationTests.Cache
{
	[TestFixture]
	public class DictionaryCachePerformanceTest : CachePerformanceTestBase
	{
		public DictionaryCachePerformanceTest() : base(new DictionaryCache())
		{
		}
	}
}
