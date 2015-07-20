using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ENTech.Store.DbEntities.PartnerModule;
using ENTech.Store.Infrastructure.Database.EF6;
using ENTech.Store.Infrastructure.Extensions;

namespace ENTech.Store.Services.AuthenticationModule
{
	public class PartnerQuery : QueryBase<PartnerDbEntity>, IPartnerQuery
	{
		public PartnerQuery(IDbSet<PartnerDbEntity> dbSet) : base(dbSet)
		{
		}

		public override IEnumerable<int> Find<TCriteria>(TCriteria criteria)
		{
			throw new System.NotImplementedException();
		}

		public int? GetByApiKey(string apiKey)
		{
			return DbSet.First(x => x.Key == apiKey)
				.Decode(x => x.Id);
		}
	}
}