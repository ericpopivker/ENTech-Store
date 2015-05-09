using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace ENTech.Store.Api.IntegrationTests
{
	[TestFixture]
	class StoreControllerTest
	{
		[Test]
		public void Create_Test()
		{
			var client = new RestClient("http://localhost:50393");
			var request = new RestRequest("1.0/store-admin-api/stores", Method.POST);
			
			request.RequestFormat = DataFormat.Json;

			var store = new {Name = "Store1", Email = "Email1", Phone = "Phone1"};

			request.AddBody(
				new
				{
					Store = store
				}
				);


			IRestResponse response = client.Execute(request);
			var responseObject = JsonConvert.DeserializeObject<dynamic>(response.Content);

			Assert.AreEqual(true, (bool)responseObject.IsSuccess);
		}
		
	}
}
