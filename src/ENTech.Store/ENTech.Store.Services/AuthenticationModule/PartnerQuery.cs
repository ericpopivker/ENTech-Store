using System;
using System.Collections.Generic;
using System.Linq;
using ENTech.Store.Database;
using ENTech.Store.Database.PartnerModule;

namespace ENTech.Store.Services.AuthenticationModule
{
	public class PartnerQuery : QueryBase<PartnerDbEntity>, IPartnerQuery
	{
		public PartnerQuery(IDbContext dbContext) : base(dbContext)
		{
		}

		public override IEnumerable<int> Find<TCriteria>(TCriteria criteria)
		{
			throw new NotImplementedException();
		}

		public int? GetByApiKey(string apiKey)
		{
			var partner =  DbSet.FirstOrDefault(x => x.Key == apiKey);
			if (partner == null)
				return null;
			return partner.Id;
		}
	}
}