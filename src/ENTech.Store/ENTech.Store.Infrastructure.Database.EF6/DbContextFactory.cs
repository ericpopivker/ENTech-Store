namespace ENTech.Store.Infrastructure.Database.EF6
{
	public class DbContextFactory : IDbContextFactory
	{
		public IDbContext Create()
		{
			return new DbContext();
		}
	}
}
