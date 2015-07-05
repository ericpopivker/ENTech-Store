using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ENTech.Store.Infrastructure.Database.Tests
{
	public class FakeDbSet<T> : IDbSet<T> where T : class
	{
		private ObservableCollection<T> _data;
		private IQueryable<T> _query;
		private Expression<Func<T, bool>> _filter;

		public FakeDbSet(ObservableCollection<T> data)
		{
			_data = data;
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
}