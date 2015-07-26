using System.Data.Entity.ModelConfiguration;
using ENTech.Store.DbEntities.OrderModule;

namespace ENTech.Store.Infrastructure.Database.EF6.Configurations.OrderModule
{
	internal sealed class OrderPaymentConfiguration : EntityTypeConfiguration<OrderPaymentDbEntity>
	{
		public OrderPaymentConfiguration()
		{
			HasOptional(x => x.Order)
				.WithOptionalDependent(y => y.Payment)
				.WillCascadeOnDelete(false);
		}
	}
}