using System.Data.Entity;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public interface IDbSetResolver
	{
		IDbSet<T> Resolve<T>() where T : class, IEntity;
	}
}