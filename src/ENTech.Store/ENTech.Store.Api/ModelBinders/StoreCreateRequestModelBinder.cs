using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using ENTech.Store.Api.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ENTech.Store.Api.ModelBinders
{
	public class StoreCreateRequestModelBinder : IModelBinder
	{
		public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			if (bindingContext.ModelType != typeof(StoreController.StoreCreateRequest))
				return false;

			var request = new StoreController.StoreCreateRequest();

			var body = actionContext.Request.Content.ReadAsStringAsync().Result;

			JObject jsonJObject = (JObject)JsonConvert.DeserializeObject(body);
			Dictionary<string, dynamic> jsonDeserialized = new Dictionary<string, object>(jsonJObject.ToObject<IDictionary<string, dynamic>>(), StringComparer.CurrentCultureIgnoreCase);
			
			if (GetValue<object>("Store", jsonDeserialized) == null)
			{
				request.Store = null;
			}
			else
			{
				request.Store = new StoreController.StoreDto();

				var store = GetValue<JObject>("Store", jsonDeserialized);

				Dictionary<string, dynamic> storeDictionary =
					new Dictionary<string, object>(store.ToObject<IDictionary<string, dynamic>>(),
						StringComparer.CurrentCultureIgnoreCase);


				request.Store.Name = GetValue<string>("Name", storeDictionary);
				request.Store.Email = GetValue<string>("Email", storeDictionary);
				request.Store.Phone = GetValue<string>("Phone", storeDictionary);
			}

			bindingContext.Model = request;
			return true;
		}


		private T GetValue<T>(string key, Dictionary<string, dynamic> dictionary) where T : class
		{
			object result = null;

			dictionary.TryGetValue(key, out result);

			return (T)result;
		}
	}
}