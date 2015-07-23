namespace ENTech.Store.Infrastructure.Database.EF6
{
	public interface IDbContextFactory
	{
		IDbContext Create();
	}
}