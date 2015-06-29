using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ENTech.Store.Infrastructure.Database.Repository
{
	public interface IQueryExecuter<out TProjection> where TProjection : IProjection
	{
		TProjection GetById(int id);
		IEnumerable<TProjection> Find();
	}
}