using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace ENTech.Store.Services.Internal.UnitTests
{
	public class DbSetFake<T> : IListSource, IFilerableDbSet<T> where T : class
	{
		private ObservableCollection<T> _data;
		private IQueryable<T> _query;
		private Expression<Func<T, bool>> _filter;

		public DbSetFake()
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

	//class InMemoryDbContext:IDbContext
	//{
	//	private FakeDbSet<Product> _products;

	//	public void Dispose()
	//	{
	//	}

	//	public IFilerableDbSet<Core.StoreModule.Store> Stores { get; set; }
	//	public IFilerableDbSet<BillableItem> BillableItems { get; set; }

	//	public IFilerableDbSet<Product> Products
	//	{
	//		get
	//		{
	//			if (_products == null) _products = new Lazy<FakeDbSet<Product>>().Value;
	//			return _products;
	//		}
	//		set { throw new NotImplementedException(); }
	//	}

	//	public IFilerableDbSet<Service> Services { get; set; }
	//	public IFilerableDbSet<ServiceCategory> ServiceCategories { get; set; }
	//	public IFilerableDbSet<ServiceSubCategory> ServiceSubCategories { get; set; }
	//	public IFilerableDbSet<User> Users { get; set; }
	//	public IFilerableDbSet<Country> Countries { get; set; }
	//	public IFilerableDbSet<State> States { get; set; }
	//	public IFilerableDbSet<Address> Addresses { get; set; }
	//	public IFilerableDbSet<Customer> Customers { get; set; }
	//	public IFilerableDbSet<Order> Orders { get; set; }
	//	public IFilerableDbSet<OrderItem> OrderItems { get; set; }
	//	public IFilerableDbSet<OrderShipping> OrderShippings { get; set; }
	//	public IFilerableDbSet<Payment> Payments { get; set; }
	//	public IDbContext LimitByStore(int storeId)
	//	{
	//		throw new NotImplementedException();
	//	}

	//	public IDbContext LimitByUser(int userId)
	//	{
	//		throw new NotImplementedException();
	//	}

	//	public int SaveChanges()
	//	{
	//		throw new NotImplementedException();
	//	}
	//}
}
