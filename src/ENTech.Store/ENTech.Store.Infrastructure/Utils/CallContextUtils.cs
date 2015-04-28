using System.Runtime.Remoting.Messaging;
using System.Web;

namespace ENTech.Store.Infrastructure.Utils
{
	/// <summary>
	/// Thread safe user context that works in both: Web and non Web environments
	/// </summary>
	public static class CallContextUtils
	{
		/// <summary>
		/// Save data into appropriate storage (Web Context or Local thread storage)
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SetData(string key, object value)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
				CallContext.SetData(key, value);
			else
				httpContext.Items[key] = value;
		}


		/// <summary>
		/// Retreive data from appropriate storage.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static object GetData(string key)
		{

			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
				return CallContext.GetData(key);
			else
				return httpContext.Items[key];
		}

		public static T GetData<T>(string key)
		{
			return (T)GetData(key);
		}

		/// <summary>
		/// Remove data identified by key from storage.
		/// </summary>
		/// <param name="key"></param>
		public static void RemoveData(string key)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
				CallContext.FreeNamedDataSlot(key);
			else
				httpContext.Items.Remove(key);
		}
	}
}
