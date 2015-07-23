using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ENTech.Store.DbEntities.CustomerModule;
using ENTech.Store.DbEntities.GeoModule;
using ENTech.Store.DbEntities.PartnerModule;
using ENTech.Store.DbEntities.StoreModule;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Database.QueryExecuter;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public class FakeDbContext : IDbContext
	{
		public void Dispose()
		{
			
		}

		public IDbSet<TEntity> DbSet<TEntity>() where TEntity : class
		{
			return new FakeDbSet<TEntity>();
		}

		public int SaveChanges()
		{
			return 0;
		}

		public void BeginTransaction()
		{
			;
		}

		public void CompleteTransaction()
		{
			;
		}

		public void RollbackTransaction()
		{
			;
		}

		public bool IsDisposed { get; private set; }
		public IFilterableDbSet<PartnerDbEntity> Partners { get; private set; }
		public IFilterableDbSet<StoreDbEntity> Stores { get; private set; }
		public IFilterableDbSet<ProductDbEntity> Products { get; private set; }
		public IFilterableDbSet<CustomerDbEntity> Customers { get; private set; }
		public IFilterableDbSet<AddressDbEntity> Addresses { get; private set; }
		public IFilterableDbSet<CountryDbEntity> Countries { get; private set; }
		public IFilterableDbSet<StateDbEntity> States { get; private set; }
		public IDbSet<T> GetDbSet<T>() where T : class, IDbEntity
		{
			return new FakeDbSet<T>();
		}

		public FakeDbContext()
		{
			Stores = new FakeDbSet<StoreDbEntity>();
		}

		public IDbContext LimitByStore(int storeId)
		{
			return this;
			;
		}

		public class FakeDbSet<T> : IListSource, IFilterableDbSet<T> where T : class
		{
			private ObservableCollection<T> _data;
			private IQueryable<T> _query;
			private Expression<Func<T, bool>> _filter;

			public FakeDbSet()
			{
				_data = new ObservableCollection<T>();
				_query = _data.AsQueryable();
				_filter = _ => true;
			}

			public virtual T Find(params object[] keyValues)
			{
				throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
			}

			public T Add(T item)
			{
				_data.Add(item);
				return item;
			}

			public T Remove(T item)
			{
				_data.Remove(item);
				return item;
			}

			public T Attach(T item)
			{
				_data.Add(item);
				return item;
			}

			public T Detach(T item)
			{
				_data.Remove(item);
				return item;
			}

			public T Create()
			{
				return Activator.CreateInstance<T>();
			}

			public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
			{
				return Activator.CreateInstance<TDerivedEntity>();
			}

			public ObservableCollection<T> Local
			{
				get { return _data; }
			}

			Type IQueryable.ElementType
			{
				get { return typeof(T); }
			}

			Expression IQueryable.Expression
			{
				get { return _query.Where(_filter).Expression; }
			}

			IQueryProvider IQueryable.Provider
			{
				get { return _query.Where(_filter).Provider; }
			}

			public void ApplyFilter(Expression<Func<T, bool>> filter)
			{
				_filter = FilterableDbSetHelper.AttachFilter(_filter, filter);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return _query.Where(_filter).GetEnumerator();
			}

			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return _query.Where(_filter).GetEnumerator();
			}

			public IList GetList()
			{
				return _data.ToList();
			}

			public bool ContainsListCollection { get { return true; } }

			public void Clear()
			{
				foreach (var entity in Local.ToList())
				{
					Remove(entity);
				}
			}
		}

		public void MarkUpdated<TEntity>(TEntity stubEntity) where TEntity : class, IDomainEntity
		{
			throw new NotImplementedException();
		}
	}
}