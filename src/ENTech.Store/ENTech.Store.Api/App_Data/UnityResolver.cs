using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace ENTech.Store.Api.App_Data
{
	public class UnityResolver : IDependencyResolver
	{
		private readonly IUnityContainer _container;

		public UnityResolver(IUnityContainer container)
		{
			_container = container;
		}

		public void Dispose()
		{
			_container.Dispose();
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return _container.Resolve(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return _container.ResolveAll(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return new List<object>();
			}
		}

		public IDependencyScope BeginScope()
		{
			var child = _container.CreateChildContainer();
			return new UnityResolver(child);
		}
	}
}