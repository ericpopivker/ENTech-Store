using System.Runtime.Remoting.Messaging;
using System.Web;

namespace ENTech.Store.Infrastructure.Context
{
	public class CallContextStorage : IContextStorage
	{
		public T Get<T>(string propertyName)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
				return (T)CallContext.GetData(propertyName);
			else
				return (T)httpContext.Items[propertyName];
		}

		public void Store<T>(string propertyName, T value)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
				CallContext.SetData(propertyName, value);
			else
				httpContext.Items[propertyName] = value;
		}

		public void Clear(string propertyName)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
				CallContext.FreeNamedDataSlot(propertyName);
			else
				httpContext.Items.Remove(propertyName);
		}

		public bool Contains(string propertyName)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
				return CallContext.GetData(propertyName) != null;
			else
				return httpContext.Items.Contains(propertyName);
		}
	}
}