using System.Reflection;

namespace ENTech.Store.Infrastructure.Mapping
{
	public static class Map
	{
		public static void InitializeAutomapperProfiles()
		{
			var assembly = Assembly.GetCallingAssembly();

			new AutoMapperBootstrapper().Execute(assembly);
		}

		public static void InitializeAutomapperProfilesInAssembly(Assembly assembly)
		{
			new AutoMapperBootstrapper().Execute(assembly);
		}
	}
}