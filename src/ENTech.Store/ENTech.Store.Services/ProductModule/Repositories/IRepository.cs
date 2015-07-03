using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Services.ProductModule.Repositories
{
	public interface IRepository<T> where T : IEntity
	{
		void Add(T entity);

		void Update(T entity);
		
		T GetById(int entityId);

		T GetById(IList<int> entityIds);

		void Delete(int entityId);
	}
}
