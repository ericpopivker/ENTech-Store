namespace ENTech.Store.Database
{
	public interface IDbContextFactory
	{
		IDbContext Create();
	}
}