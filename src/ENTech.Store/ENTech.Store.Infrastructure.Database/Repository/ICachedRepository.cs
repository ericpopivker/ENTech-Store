using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository
{
	//a workaround for fast inject
	public interface ICachedRepository<T> : IRepository<T> where T : IDomainEntity
	{
		
	}
}