using System.Data.Entity.ModelConfiguration.Conventions;

namespace ENTech.Store.Infrastructure.Database.EF6.Conventions
{
	public class IdConvention : Convention
	{
		public IdConvention()
		{
			Properties<int>()
				.Where(p => p.Name.Equals("Id"))
				.Configure(p => p.IsKey()); 
		}  
	}
}