using System;
using System.Data.Entity;
using ENTech.Store.DbEntities.CustomerModule;
using ENTech.Store.DbEntities.GeoModule;
using ENTech.Store.DbEntities.PartnerModule;
using ENTech.Store.DbEntities.StoreModule;
using ENTech.Store.Infrastructure.Database.Entities;
using ENTech.Store.Infrastructure.Database.QueryExecuter;

namespace ENTech.Store.Infrastructure.Database.EF6
{
	public interface IDbContext : IDisposable
	{
		int SaveChanges();

		void BeginTransaction();

		void CompleteTransaction();

		void RollbackTransaction();

		bool IsDisposed { get; }

		IFilterableDbSet<PartnerDbEntity> Partners { get; }

		IFilterableDbSet<StoreDbEntity> Stores { get; }

		IFilterableDbSet<ProductDbEntity> Products { get; }

		IFilterableDbSet<CustomerDbEntity> Customers { get; }

		IFilterableDbSet<AddressDbEntity> Addresses { get; }

		IFilterableDbSet<CountryDbEntity> Countries { get; }

		IFilterableDbSet<StateDbEntity> States { get; }

		IDbSet<T> GetDbSet<T>() where T : class, IDbEntity;

		IDbContext LimitByStore(int storeId);
	}
}