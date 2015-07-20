using ENTech.Store.Infrastructure.Database.EF6;

namespace ENTech.Store.Api.App_Data
{
	public class DbContextFactory : IDbContextFactory
	{
		public IDbContext Create()
		{
			return new FakeDbContext();
		}
	}
}
