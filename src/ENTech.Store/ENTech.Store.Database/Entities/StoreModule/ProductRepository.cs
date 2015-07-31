using System.Data.Entity;
using System.Linq;
using ENTech.Store.Database.Utility;
using ENTech.Store.Entities.StoreModule;

namespace ENTech.Store.Database.Entities.StoreModule
{
	public class ProductRepository : Repository<Product, ProductDbEntity>
	{
		public ProductRepository(IDbContext dbContext, IDbEntityStateKeeper<Product, ProductDbEntity> dbEntityStateKeeper, IDbEntityMapper dbEntityMapper) : base(dbContext, dbEntityStateKeeper, dbEntityMapper)
		{
		}

		protected override IQueryable<ProductDbEntity> ApplyIncludes(IDbSet<ProductDbEntity> dbSet)
		{
			return dbSet.Include(x => x.Category);
		}
	}
}