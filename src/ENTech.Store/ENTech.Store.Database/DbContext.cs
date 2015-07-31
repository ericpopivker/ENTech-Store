using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
using ENTech.Store.Database.Conventions;
using ENTech.Store.Database.Entities.CustomerModule;
using ENTech.Store.Database.Entities.CustomerModule.Configurations;
using ENTech.Store.Database.Entities.GeoModule;
using ENTech.Store.Database.Entities.GeoModule.Configurations;
using ENTech.Store.Database.Entities.OrderModule.Configurations;
using ENTech.Store.Database.Entities.PartnerModule;
using ENTech.Store.Database.Entities.PartnerModule.Configurations;
using ENTech.Store.Database.Entities.StoreModule;
using ENTech.Store.Database.Entities.StoreModule.Configurations;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Utils;

namespace ENTech.Store.Database
{
	public class DbContext : System.Data.Entity.DbContext, IDbContext
	{
		private IFilterableDbSet<CountryDbEntity> _countries;
		private IFilterableDbSet<StateDbEntity> _states;
		private IFilterableDbSet<AddressDbEntity> _addresses;

		private IFilterableDbSet<CustomerDbEntity> _customers;
		private IFilterableDbSet<StoreDbEntity> _stores;

		private IFilterableDbSet<PartnerDbEntity> _partners;
		private IFilterableDbSet<ProductDbEntity> _products;
		private IFilterableDbSet<ProductCategoryDbEntity> _productCategories;

		private DbContextTransaction _transaction;

		public DbContext()
			: base(EnvironmentUtils.GetConnectionStringName())
		{
			_partners = new Lazy<IFilterableDbSet<PartnerDbEntity>>(() => new FilterableDbSet<PartnerDbEntity>(this)).Value;

			_stores = new Lazy<IFilterableDbSet<StoreDbEntity>>(() => new FilterableDbSet<StoreDbEntity>(this)).Value;

			_customers = new Lazy<IFilterableDbSet<CustomerDbEntity>>(() => new FilterableDbSet<CustomerDbEntity>(this)).Value;

			_countries = new Lazy<IFilterableDbSet<CountryDbEntity>>(() => new FilterableDbSet<CountryDbEntity>(this)).Value;
			_states = new Lazy<IFilterableDbSet<StateDbEntity>>(() => new FilterableDbSet<StateDbEntity>(this)).Value;
			_addresses = new Lazy<IFilterableDbSet<AddressDbEntity>>(() => new FilterableDbSet<AddressDbEntity>(this)).Value;

			_products = new Lazy<IFilterableDbSet<ProductDbEntity>>(() => new FilterableDbSet<ProductDbEntity>(this)).Value;

			_productCategories = new Lazy<IFilterableDbSet<ProductCategoryDbEntity>>(() => new FilterableDbSet<ProductCategoryDbEntity>(this)).Value;
		}

		public bool ValidateEntity<TEntity>(TEntity entity) where TEntity : class, IDbEntity
		{
			DbEntityValidationResult res = Entry(entity).GetValidationResult();
			if (!res.IsValid)
			{
				var e = new DbEntityValidationException
					("Entity validation failed", new List<DbEntityValidationResult> { res });
				throw e;
			}
			return true;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Add(new IdConvention());
			modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());
			modelBuilder.Conventions.Add(new TableNameConvention());

			modelBuilder.Configurations.Add(new OrderItemConfiguration());
			modelBuilder.Configurations.Add(new OrderConfiguration());
			modelBuilder.Configurations.Add(new OrderShippingConfiguration());
			modelBuilder.Configurations.Add(new OrderPaymentConfiguration());
			modelBuilder.Configurations.Add(new StoreConfiguration());
			modelBuilder.Configurations.Add(new ProductConfiguration());
			modelBuilder.Configurations.Add(new CustomerConfiguration());
			modelBuilder.Configurations.Add(new AddressConfiguration());
			modelBuilder.Configurations.Add(new StateConfiguration());
			modelBuilder.Configurations.Add(new CountryConfiguration());
			modelBuilder.Configurations.Add(new PartnerConfiguration());
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

		public bool IsDisposed { get; private set; }

		public IFilterableDbSet<PartnerDbEntity> Partners
		{
			get { return _partners; }
		}
		public IFilterableDbSet<StoreDbEntity> Stores
		{
			get { return _stores; }
		}
		public IFilterableDbSet<ProductDbEntity> Products
		{
			get { return _products; }
		}
		public IFilterableDbSet<CustomerDbEntity> Customers
		{
			get { return _customers; }
		}
		public IFilterableDbSet<AddressDbEntity> Addresses
		{
			get { return _addresses; }
		}
		public IFilterableDbSet<CountryDbEntity> Countries
		{
			get { return _countries; }
		}
		public IFilterableDbSet<StateDbEntity> States
		{
			get { return _states; }
		}

		public IFilterableDbSet<ProductCategoryDbEntity> ProductCategories
		{
			get { return _productCategories; }
		}

		public IDbSet<T> GetDbSet<T>() where T : class, IDbEntity
		{
			return Set<T>();
		}

		public IDbContext LimitByStore(int storeId)
		{
			Customers.ApplyFilter(p => p.StoreId == storeId);
			return this;
		}
	}
}