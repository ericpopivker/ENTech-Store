using System.Data.Entity;
using System.Linq;
using ENTech.Store.Database.Utility;

namespace ENTech.Store.Database.Entities.StoreModule
{
	public class StoreRepository : Repository<Store.Entities.StoreModule.Store, StoreDbEntity>
	{
		public StoreRepository(IDbContext dbContext, IDbEntityStateKeeper<Store.Entities.StoreModule.Store, StoreDbEntity> dbEntityStateKeeper, IDbEntityMapper dbEntityMapper) : base(dbContext, dbEntityStateKeeper, dbEntityMapper)
		{
		}

		protected override IQueryable<StoreDbEntity> ApplyIncludes(IDbSet<StoreDbEntity> dbSet)
		{
			return dbSet.Include(x => x.Products);
		}
	}
}