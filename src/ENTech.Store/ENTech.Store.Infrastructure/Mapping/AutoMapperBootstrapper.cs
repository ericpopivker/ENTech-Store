using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace ENTech.Store.Infrastructure.Mapping
{
	public class AutoMapperBootstrapper
	{
		public void Execute(Assembly assemblyLoad)
		{
			AutoMapper.Mapper.Reset();

			var assemblyNames = assemblyLoad.GetReferencedAssemblies();

			foreach (var assemblyName in assemblyNames)
			{
				var needToLoad = AppDomain.CurrentDomain
									 .GetAssemblies()
									 .Any(assembly => AssemblyName.ReferenceMatchesDefinition(
										 assemblyName, assembly.GetName())) == false;

				if (needToLoad)
					Assembly.Load(assemblyName);
			}


			var profiles = AppDomain.CurrentDomain
				.GetAssemblies()
				.Where(assembly => assembly
					.GetCustomAttributes(typeof(AutoMappingProfilesContainerAttribute), true)
					.Any())
				.SelectMany(assembly => GetTypes(assembly)
					.Where(type => type.IsSubclassOf(typeof(Profile))));



			foreach (var instance in profiles.Select(profile => Activator.CreateInstance(profile) as Profile))
				AutoMapper.Mapper.AddProfile(instance);
		}

		private Type[] GetTypes(Assembly assembly)
		{
			return assembly.GetTypes();
		}
	}
}