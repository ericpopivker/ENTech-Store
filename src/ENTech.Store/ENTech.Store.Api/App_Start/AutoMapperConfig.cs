using ENTech.Store.Infrastructure.Mapping;

namespace ENTech.Store.Api
{
	public class AutoMapperConfig
	{
		public static void RegisterComponents()
		{
			Map.InitializeAutomapperProfiles();
		}
	}
}