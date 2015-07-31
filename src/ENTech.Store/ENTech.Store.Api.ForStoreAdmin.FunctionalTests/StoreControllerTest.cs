using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using ENTech.Store.Services.StoreModule.Expand.Dtos;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace ENTech.Store.Api.ForStoreAdmin.FunctionalTests
{
	public class StoreControllerTest
	{
		public delegate void T(int n);

		[Test]
		public void Create_When_valid_data_Then_returns_ok_response([Values(1, 10, 30, 50, 200 ,1000)]int readonlyThreads)
		{
			var client = new RestClient("http://localhost:50393");
			
			var threadCount = 100;
			var testStartTime = DateTime.UtcNow;

			var testDuration = DateTime.UtcNow - testStartTime;
			Console.WriteLine(testDuration.TotalMilliseconds);

			List<double> _durationsList = new List<double>();

			var connections = 0;
			T readlonlyGet = (n) =>
			{
				connections += 1;
				var start = System.DateTime.UtcNow;
				try
				{
					var request = new RestRequest("v1/store-admin-api/stores/4/expand", Method.GET);
					request.AddParameter("ReturnHttpStatusCode", 200); // adds to POST or URL querystring based on Method
					request.AddHeader("Authorization", "Basic ApiKey=s3cr3tk3y UserToken=123");
					// execute the request
					IRestResponse restResponse = client.Execute(request);
					Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode);
				}
				catch
				{
				}

				_durationsList.Add((DateTime.UtcNow - start).TotalMilliseconds);
			};
			IAsyncResult[] resultsReadonly = new IAsyncResult[readonlyThreads];

			for (int i = 0; i < readonlyThreads; i++)
			{
				resultsReadonly[i] = readlonlyGet.BeginInvoke(i, null, null);
			}

			for (int i = 0; i < readonlyThreads; i++)
			{
			
				readlonlyGet.EndInvoke(resultsReadonly[i]);
			}

			Console.WriteLine("Average call time: {0} ms", _durationsList.Average());
			Console.WriteLine("Min call time: {0} ms", _durationsList.Min());
			Console.WriteLine("Max call time: {0} ms", _durationsList.Max());
		} 
	}
}