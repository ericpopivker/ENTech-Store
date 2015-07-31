namespace ENTech.Store.Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ProductCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Store", t => t.StoreId)
                .Index(t => t.StoreId, name: "IX_Store_Id");
            
            AddColumn("dbo.Product", "CategoryId", c => c.Int());
            CreateIndex("dbo.Product", "CategoryId", name: "IX_Category_Id");
            AddForeignKey("dbo.Product", "CategoryId", "dbo.ProductCategory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCategory", "StoreId", "dbo.Store");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.ProductCategory");
            DropIndex("dbo.Product", "IX_Category_Id");
            DropIndex("dbo.ProductCategory", "IX_Store_Id");
            DropColumn("dbo.Product", "CategoryId");
            DropTable("dbo.ProductCategory");
        }
    }
}
