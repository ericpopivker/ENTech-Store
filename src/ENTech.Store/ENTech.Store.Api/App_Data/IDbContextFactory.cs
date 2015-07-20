using ENTech.Store.Infrastructure.Database.EF6;

namespace ENTech.Store.Api.App_Data
{
	public interface IDbContextFactory
	{
		IDbContext Create();
	}
}