namespace ENTech.Store.Entities
{
	public class DbContextFactory : IDbContextFactory
	{
		public IDbContext Create()
		{
			return new FakeDbContext();
		}
	}
}
