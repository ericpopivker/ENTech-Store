using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Entities.PartnerModule;
using ENTech.Store.Entities.CustomerModule;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Infrastructure.Entities;

namespace ENTech.Store.Entities
{
	public interface IDbContext : IDisposable
	{

		IDbSet<TEntity> DbSet<TEntity>() where TEntity : class;

		int SaveChanges();

		void BeginTransaction();

		void CompleteTransaction();

		void RollbackTransaction();

		bool IsDisposed { get; }


		IFilerableDbSet<Partner> Partners { get; }

		IFilerableDbSet<StoreModule.Store> Stores { get; }

		IFilerableDbSet<StoreModule.Product> Products { get; }

		IFilerableDbSet<Customer> Customers { get; }

		IFilerableDbSet<Address> Addresses { get; }

		IFilerableDbSet<Country> Countries { get; }
		
		IFilerableDbSet<State> States { get; }

		IDbSet<T> GetDbSet<T>() where T : class, IEntity;

		IDbContext LimitByStore(int storeId);
	}

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
		public IFilerableDbSet<Partner> Partners { get; private set; }
		public IFilerableDbSet<StoreModule.Store> Stores { get; private set; }
		public IFilerableDbSet<Product> Products { get; private set; }
		public IFilerableDbSet<Customer> Customers { get; private set; }
		public IFilerableDbSet<Address> Addresses { get; private set; }
		public IFilerableDbSet<Country> Countries { get; private set; }
		public IFilerableDbSet<State> States { get; private set; }
		public IDbSet<T> GetDbSet<T>() where T : class, IEntity
		{
			return new FakeDbSet<T>();
		}

		public IDbContext LimitByStore(int storeId)
		{
			return this;
			;
		}

		public class FakeDbSet<T> : IListSource, IFilerableDbSet<T> where T : class
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

			System.Linq.Expressions.Expression IQueryable.Expression
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

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
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

	}



	public interface IFilerableDbSet<TEntity> : IDbSet<TEntity> where TEntity:class
	{
		void ApplyFilter(Expression<Func<TEntity, bool>> filter);
	}

	public static class FilterableDbSetHelper
	{
		private class ReplaceExpressionVisitor : ExpressionVisitor
		{
			private readonly Expression _oldValue;
			private readonly Expression _newValue;

			public ReplaceExpressionVisitor(Expression newValue, Expression oldValue)
			{
				_newValue = newValue;
				_oldValue = oldValue;
			}

			public override Expression Visit(Expression node)
			{
				if (node == _oldValue)
					return _newValue;
				return base.Visit(node);
			}
		}

		public static Expression<Func<TEntity, bool>> AttachFilter<TEntity>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, bool>> filterToAttach)
		{
			var argument = Expression.Parameter(typeof(TEntity));
			var leftVisitor = new ReplaceExpressionVisitor(filter.Parameters[0], argument);
			var left = leftVisitor.Visit(filter.Body);

			var rightVisitor = new ReplaceExpressionVisitor(filterToAttach.Parameters[0], argument);
			var right = rightVisitor.Visit(filterToAttach.Body);

			return Expression.Lambda<Func<TEntity, bool>>(
				Expression.AndAlso(left, right), argument);
		}
	}	

	public class FilterableDbSet<TEntity> : IFilerableDbSet<TEntity>, IOrderedQueryable<TEntity>, IListSource where TEntity : class
	{
		private readonly DbSet<TEntity> _set;
		private Expression<Func<TEntity, bool>> _filter;

		public FilterableDbSet(System.Data.Entity.DbContext context)
			: this(context, _ => true)
		{
		}

		public FilterableDbSet(System.Data.Entity.DbContext context, Expression<Func<TEntity, bool>> filter)
			: this(context.Set<TEntity>(), filter)
		{
		}

		private FilterableDbSet(DbSet<TEntity> set, Expression<Func<TEntity, bool>> filter)
		{
			_set = set;
			_filter = filter;
		}

		public IQueryable<TEntity> Include(string path)
		{
			return _set.Include(path).Where(_filter).AsQueryable();
		}

		public TEntity Add(TEntity entity)
		{
			return _set.Add(entity);
		}

		public void AddOrUpdate(TEntity entity)
		{
			_set.AddOrUpdate(entity);
		}

		public TEntity Attach(TEntity entity)
		{
			return _set.Attach(entity);
		}

		public TEntity Create()
		{
			var entity = _set.Create();
			return entity;
		}

		public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
		{
			var entity = _set.Create<TDerivedEntity>();
			return entity;
		}

		[Obsolete("Using is denied", true)]
		public TEntity Find(params object[] keyValues)
		{
			throw new InvalidOperationException();
		}

		public TEntity Remove(TEntity entity)
		{
			if (!_set.Local.Contains(entity))
			{
				_set.Attach(entity);
			}
			return _set.Remove(entity);
		}

		public ObservableCollection<TEntity> Local
		{
			get { return _set.Local; }
		}

		IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
		{
			return _set.Where(_filter).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _set.Where(_filter).GetEnumerator();
		}

		Type IQueryable.ElementType
		{
			get { return typeof (TEntity); }
		}

		Expression IQueryable.Expression
		{
			get { return _set.Where(_filter).Expression; }
		}

		IQueryProvider IQueryable.Provider
		{
			get { return _set.Where(_filter).AsQueryable().Provider; }
		}

		bool IListSource.ContainsListCollection
		{
			get { return false; }
		}

		[Obsolete("Using is denied", true)]
		IList IListSource.GetList()
		{
			throw new InvalidOperationException();
		}

		public void ApplyFilter(Expression<Func<TEntity, bool>> filter)
		{
			_filter = FilterableDbSetHelper.AttachFilter(_filter, filter);
		}
	}
}