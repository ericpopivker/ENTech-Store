using System.Data.Entity;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database
{
	public interface IDbSetResolver
	{
		IDbSet<T> Resolve<T>() where T : class, IEntity;
	}
}