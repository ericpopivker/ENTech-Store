using ENTech.Store.Infrastructure.Database.Repository;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Tests
{
	public class RepositoryTests
	{
		private IRepository<StubEntity> _repository = new Repository<StubEntity>();
	}

	public class StubEntity : IEntity
	{
		public int Id { get; set; }
	}
}