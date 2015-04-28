
namespace ENTech.Store.Entities
{
	class DbContextFactory : IDbContextFactory
	{
		public IDbContext Create()
		{
			return new DbContext();
		}
	}
}
