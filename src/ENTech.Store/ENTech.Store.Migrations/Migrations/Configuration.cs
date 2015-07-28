using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace ENTech.Store.Migrations.Migrations
{
	internal sealed class Configuration : DbMigrationsConfiguration<DbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			SetSqlGenerator("System.Data.SqlClient", new SqlGenerator());
		}

		protected override void Seed(DbContext context)
		{

		}
	}
}
