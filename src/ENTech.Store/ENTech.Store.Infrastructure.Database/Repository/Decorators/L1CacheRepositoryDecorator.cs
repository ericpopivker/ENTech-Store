using System;
using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.Repository.Decorators
{

	public class L1CacheRepositoryDecorator<TEntity> : CacheRepositoryDecoratorBase<TEntity>
		where TEntity : class, IDomainEntity, IAuditable
	{
		public L1CacheRepositoryDecorator(IRepository<TEntity> baseRepository, ICache cache) : base(baseRepository, cache)
		{
		}
	}

}
