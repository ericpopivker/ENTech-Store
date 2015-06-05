using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
using ENTech.Store.Entities.GeoModule;
using ENTech.Store.Entities.PartnerModule;
using ENTech.Store.Entities.CustomerModule;
using ENTech.Store.Entities.StoreModule;
using ENTech.Store.Infrastructure.Cache;
using ENTech.Store.Infrastructure.Entities;
using ENTech.Store.Infrastructure.Utils;

namespace ENTech.Store.Entities
{
	public class DbContext : System.Data.Entity.DbContext, IDbContext
	{

		private IFilerableDbSet<Country> _countries;
		private IFilerableDbSet<State> _states;
		private IFilerableDbSet<Address> _addresses;

		private IFilerableDbSet<Customer> _customers;
		private IFilerableDbSet<StoreModule.Store> _stores;

		private DbContextTransaction _transaction;
		private IFilerableDbSet<Partner> _partners;
		private IFilerableDbSet<Product> _products;

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
			_partners = new Lazy<IFilerableDbSet<Partner>>(() => new FilterableDbSet<Partner>(this)).Value;

			_stores = new Lazy<IFilerableDbSet<StoreModule.Store>>(() => new FilterableDbSet<StoreModule.Store>(this)).Value;

			_customers = new Lazy<IFilerableDbSet<Customer>>(() => new FilterableDbSet<Customer>(this)).Value;

			_countries = new Lazy<IFilerableDbSet<Country>>(() => new FilterableDbSet<Country>(this)).Value;
			_states = new Lazy<IFilerableDbSet<State>>(() => new FilterableDbSet<State>(this)).Value;
			_addresses = new Lazy<IFilerableDbSet<Address>>(() => new FilterableDbSet<Address>(this)).Value;

			_products = new Lazy<IFilerableDbSet<Product>>(() => new FilterableDbSet<Product>(this)).Value;

		}


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}



		public bool ValidateEntity<TEntity>(TEntity entity) where TEntity : class
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


		public IFilerableDbSet<Partner> Partners
		{
			get { return _partners; }
		}

		public IFilerableDbSet<StoreModule.Store> Stores
		{
			get { return _stores; }
		}

		public IFilerableDbSet<Product> Products
		{
			get { return _products; }
		}

		public IFilerableDbSet<Customer> Customers
		{
			get { return _customers; }
		}



		public IFilerableDbSet<Address> Addresses
		{
			get { return _addresses; }
		}

		public IFilerableDbSet<Country> Countries
		{
			get { return _countries; }
		}


		public IFilerableDbSet<State> States
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
	}
}



