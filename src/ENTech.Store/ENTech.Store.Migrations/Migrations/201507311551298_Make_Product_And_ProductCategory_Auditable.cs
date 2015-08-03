namespace ENTech.Store.Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Make_Product_And_ProductCategory_Auditable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategory", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductCategory", "LastUpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategory", "LastUpdatedAt");
            DropColumn("dbo.ProductCategory", "CreatedAt");
        }
    }
}
