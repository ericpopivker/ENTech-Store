using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Entities.PartnerModule;
using ENTech.Store.Entities.CustomerModule;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Database.EF6.Utility;
using ENTech.Store.Infrastructure.Entities;
using ENTech.Store.Infrastructure.Utils;

namespace ENTech.Store.Entities
{
	public class DbContext : System.Data.Entity.DbContext, IDbContext, IDbEntityStateManager
	{

		private IFilterableDbSet<Country> _countries;
		private IFilterableDbSet<State> _states;
		private IFilterableDbSet<Address> _addresses;

		private IFilterableDbSet<Customer> _customers;
		private IFilterableDbSet<StoreModule.Store> _stores;

		private DbContextTransaction _transaction;
		private IFilterableDbSet<Partner> _partners;
		private IFilterableDbSet<Product> _products;

		public DbContext() : base(EnvironmentUtils.GetConnectionStringName())
		{
			InitDbSets();
		}

		public IDbSet<TEntity> GetDbSet<TEntity>()
			where TEntity : class, IEntity
		{
			return Set<TEntity>();
		}

		private void InitDbSets()
		{
			_partners = new Lazy<IFilterableDbSet<Partner>>(() => new FilterableDbSet<Partner>(this)).Value;

			_stores = new Lazy<IFilterableDbSet<StoreModule.Store>>(() => new FilterableDbSet<StoreModule.Store>(this)).Value;

			_customers = new Lazy<IFilterableDbSet<Customer>>(() => new FilterableDbSet<Customer>(this)).Value;

			_countries = new Lazy<IFilterableDbSet<Country>>(() => new FilterableDbSet<Country>(this)).Value;
			_states = new Lazy<IFilterableDbSet<State>>(() => new FilterableDbSet<State>(this)).Value;
			_addresses = new Lazy<IFilterableDbSet<Address>>(() => new FilterableDbSet<Address>(this)).Value;

			_products = new Lazy<IFilterableDbSet<Product>>(() => new FilterableDbSet<Product>(this)).Value;
		}

		public bool ValidateEntity<TEntity>(TEntity entity) where TEntity : class, IEntity
		{
			DbEntityValidationResult res = Entry(entity).GetValidationResult();
			if (!res.IsValid)
			{
				var e = new DbEntityValidationException
					("Entity validation failed", new List<DbEntityValidationResult> {res});
				throw e;
			}
			return true;
		}

		public override int SaveChanges()
		{
			try
			{
				return base.SaveChanges();
			}
			catch (DbEntityValidationException e)
			{
				var sb = new StringBuilder();
				foreach (var eve in e.EntityValidationErrors)
				{
					sb.AppendFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: {2}",
						eve.Entry.Entity.GetType().Name, eve.Entry.State, Environment.NewLine);
					foreach (var ve in eve.ValidationErrors)
					{
						sb.AppendFormat("- Property: \"{0}\", Error: \"{1}\"{2}",
							ve.PropertyName, ve.ErrorMessage, Environment.NewLine);
					}
				}
				throw new DbEntityValidationException(sb.ToString(), e);
			}
		}

		public void BeginTransaction()
		{
			if (_transaction != null)
				throw new InvalidOperationException("TransactionScope already initialized");

			_transaction = Database.BeginTransaction();
		}

		public void CompleteTransaction()
		{
			if (_transaction == null)
				throw new InvalidOperationException("TransactionScope was not initialized");

			_transaction.Commit();
		}

		public void RollbackTransaction()
		{
			if (_transaction == null)
				throw new InvalidOperationException("TransactionScope was not initialized");

			_transaction.Rollback();
		}

		protected override void Dispose(bool disposing)
		{
			if (_transaction != null)
			{
				_transaction.Dispose();
				_transaction = null;
			}
			base.Dispose(disposing);
			IsDisposed = true;
		}
		
		public IFilterableDbSet<Partner> Partners
		{
			get { return _partners; }
		}

		public IFilterableDbSet<StoreModule.Store> Stores
		{
			get { return _stores; }
		}

		public IFilterableDbSet<Product> Products
		{
			get { return _products; }
		}

		public IFilterableDbSet<Customer> Customers
		{
			get { return _customers; }
		}

		public IFilterableDbSet<Address> Addresses
		{
			get { return _addresses; }
		}

		public IFilterableDbSet<Country> Countries
		{
			get { return _countries; }
		}
		
		public IFilterableDbSet<State> States
		{
			get { return _states; }
		}
		
		public IDbContext LimitByStore(int storeId)
		{
			Customers.ApplyFilter(p => p.StoreId == storeId);
			return this;
		}
		
		public bool IsDisposed { get; private set; }

		public IDbSet<TEntity> DbSet<TEntity>() where TEntity : class
		{
			return Set<TEntity>();
		}

		public void MarkUpdated<TEntity>(TEntity stubEntity) where TEntity : class, IEntity
		{
			var entry = Entry(stubEntity);
			entry.State = EntityState.Modified;
		}
	}
}