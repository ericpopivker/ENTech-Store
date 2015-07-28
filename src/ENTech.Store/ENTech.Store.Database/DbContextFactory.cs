namespace ENTech.Store.Database
{
	public class DbContextFactory : IDbContextFactory
	{
		public IDbContext Create()
		{
			return new DbContext();
		}
	}
}
