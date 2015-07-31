//using System;
//using System.Collections.Generic;
//using ENTech.Store.Infrastructure.Cache;
//using NUnit.Framework;
//using StackExchange.Profiling;

//namespace ENTech.Store.Infrastructure.IntegrationTests.Cache
//{
//	[TestFixture]
//	public abstract class CachePerformanceTestBase
//	{

//		private ICache _cache;

//		[SetUp]
//		public void SetUp()
//		{
//			//ConsoleProfiling.Start();

//			Warmup();

//		}

//		[TearDown]
//		public void TearDown()
//		{
//			var friendlyString = ConsoleProfiling.StopAndGetConsoleFriendlyOutputStringWithSqlTimings();
//			Console.WriteLine(friendlyString);

//			_cache.RemoveAll();
//		}


//		public CachePerformanceTestBase(ICache cache)
//		{
//			_cache = cache;
//		}


//		[Test]
//		public void Get_When_100_Keys_Then_view_stats()
//		{
//			int maxIndex = 100;

//			using (MiniProfiler.StepStatic("Fill Keys"))
//			{
//				for (int i = 1; i <= maxIndex; i++)
//					_cache.Set("Key" + i, "Value" + i);
//			}

//			using (MiniProfiler.StepStatic("Get Key Values"))
//			{
//				for (int i = 1; i <= maxIndex; i++)
//				{
//					string value = _cache.Get<string>("Key" + i);
//				}
//			}

//		}


//		[Test]
//		public void FindByKeys_When_100_keys_Then_faster_than_1_by_1()
//		{
//			int maxIndex = 100;

//			using (MiniProfiler.StepStatic("Fill Keys"))
//			{
//				for (int i = 1; i <= maxIndex; i++)
//					_cache.Set("Key" + i, "Value" + i);
//			}

//			IDictionary<string, string> values;
//			for (int i = 20; i <= maxIndex; i = i + 20)
//				values = GetKeyValues(i);
//		}

//		private IDictionary<string, string> GetKeyValues(int maxIndex)
//		{
//			var keys = new List<string>();
//			for (int i = 1; i <= maxIndex; i++)
//			{
//				keys.Add("Key" + i);
//			}

//			var values = _cache.FindByKeys<string>(keys);
//			return values;
//		}


//		//First set and get is a much slower then others. So remove from the equation
//		private void Warmup()
//		{
//			using (MiniProfiler.StepStatic("Warmup"))
//			{
//				_cache.Set("Key0", "Value0");

//				string value = _cache.Get<string>("Key0");

//				var values = _cache.FindByKeys<string>(new[] { "Key0" });
//			}
//		}
//	}
//}
