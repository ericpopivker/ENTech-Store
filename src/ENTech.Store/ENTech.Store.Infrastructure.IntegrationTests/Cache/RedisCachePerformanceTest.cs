using System;
using System.Collections.Generic;
using ENTech.Store.Infrastructure.Cache;
using MiniProfiler.Windows;
using NUnit.Framework;

namespace ENTech.Store.Infrastructure.IntegrationTests.Cache
{
	[TestFixture]
	public class RedisCachePerformanceTest
	{

		private RedisCache _redisCache;

		[SetUp]
		public void SetUp()
		{
			ConsoleProfiling.Start();

			Warmup();
		
		}

		[TearDown]
		public void TearDown()
		{
			var friendlyString = ConsoleProfiling.StopAndGetConsoleFriendlyOutputStringWithSqlTimings();
			Console.WriteLine(friendlyString);
				
			_redisCache.RemoveAll();
		}


		public RedisCachePerformanceTest()
		{
			_redisCache = new RedisCache();
		}


		[Test]
		public void Get_When_100_Keys_Then_view_stats()
		{
			int maxIndex = 100;
				
			using (StackExchange.Profiling.MiniProfiler.StepStatic("Fill Keys"))
			{
				for (int i = 1; i <= maxIndex; i++)
					_redisCache.Set("Key" + i, "Value" + i);
			}

			using (StackExchange.Profiling.MiniProfiler.StepStatic("Get Key Values"))
			{
				for (int i = 1; i <= maxIndex; i++)
				{
					string value = _redisCache.Get<string>("Key" + i);
				}
			}

		}


		[Test]
		public void FindByKeys_When_100_keys_Then_faster_than_1_by_1()
		{
			int maxIndex = 100;
			
			using (StackExchange.Profiling.MiniProfiler.StepStatic("Fill Keys"))
			{
				for (int i = 1; i <= maxIndex; i++)
					_redisCache.Set("Key" + i, "Value" + i);
			}

			IDictionary<string, string> values;
			for (int i = 20; i <= maxIndex; i = i + 20)
				values = GetKeyValues(i);
		}

		private IDictionary<string, string> GetKeyValues(int maxIndex)
		{
			var keys = new List<string>();
			for (int i = 1; i <= maxIndex; i++)
			{
				keys.Add("Key" + i);
			}

			var values = _redisCache.FindByKeys<string>(keys);
			return values;
		}


		//First set and get is a much slower then others. So remove from the equation
		private void Warmup()
		{
			using(StackExchange.Profiling.MiniProfiler.StepStatic("Warmup"))
			{
				_redisCache.Set("Key0", "Value0");

				string value = _redisCache.Get<string>("Key0");

				var values = _redisCache.FindByKeys<string>(new[] {"Key0"});
			}
		}
	}
}
