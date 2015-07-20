namespace ENTech.Store.Migrations.Migrations
{
	using System;
	using System.Data.Entity.Migrations;

	public partial class Insert_Test_Partner : DbMigration
	{
		public override void Up()
		{
			Sql(@"INSERT INTO [dbo].[Partner]
				 ([Name], [Key], [Secret], [CreatedAt], [LastUpdatedAt])
				VALUES
				 ('Test', 's3cr3tk3y', 'MOsYRhsSMOLg6zz7lGxZ', getdate(), getdate())");
		}

		public override void Down()
		{
		}
	}
}
