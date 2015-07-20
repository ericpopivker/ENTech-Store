using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public interface IFilterableDbSet<TEntity> : IDbSet<TEntity> where TEntity : class
	{
		void ApplyFilter(Expression<Func<TEntity, bool>> filter);
	}

	public class FilterableDbSet<TEntity> : IFilterableDbSet<TEntity>, IOrderedQueryable<TEntity>, IListSource where TEntity : class, IDbEntity
	{
		private readonly IDbSet<TEntity> _set;
		private Expression<Func<TEntity, bool>> _filter;

		public FilterableDbSet(IDbContext context)
			: this(context, _ => true)
		{
		}

		public FilterableDbSet(IDbContext context, Expression<Func<TEntity, bool>> filter)
			: this(context.GetDbSet<TEntity>(), filter)
		{
		}

		private FilterableDbSet(IDbSet<TEntity> set, Expression<Func<TEntity, bool>> filter)
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
			get { return typeof(TEntity); }
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
			var leftVisitor = new ReplaceExpressionVisitor(argument, filter.Parameters[0]);
			var left = leftVisitor.Visit(filter.Body);

			var rightVisitor = new ReplaceExpressionVisitor(argument, filterToAttach.Parameters[0]);
			var right = rightVisitor.Visit(filterToAttach.Body);

			var temp = Expression.Lambda<Func<TEntity, bool>>(
				Expression.AndAlso(left, right), argument);

			return temp;
		}
	}
}