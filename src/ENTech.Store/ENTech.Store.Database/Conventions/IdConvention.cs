using System.Data.Entity.ModelConfiguration.Conventions;

namespace ENTech.Store.Database.Conventions
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