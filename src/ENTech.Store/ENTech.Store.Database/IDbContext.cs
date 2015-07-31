using System;
using System.Data.Entity;
using ENTech.Store.Database.Entities.CustomerModule;
using ENTech.Store.Database.Entities.GeoModule;
using ENTech.Store.Database.Entities.PartnerModule;
using ENTech.Store.Database.Entities.StoreModule;
using ENTech.Store.Infrastructure.Database.Entities;

namespace ENTech.Store.Database
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