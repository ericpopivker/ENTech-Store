namespace ENTech.Store.Entities
{
	public interface IDbContextFactory
	{
		IDbContext Create();
	}

}
