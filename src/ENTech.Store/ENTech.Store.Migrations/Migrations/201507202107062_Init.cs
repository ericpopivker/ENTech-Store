namespace ENTech.Store.Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        LastUpdatedAt = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StoreId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.Store", t => t.StoreId)
                .Index(t => t.StoreId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        LastUpdatedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 20),
                        StoreId = c.Int(nullable: false),
                        BillingAddressId = c.Int(),
                        ShippingAddressId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.BillingAddressId)
                .ForeignKey("dbo.Address", t => t.ShippingAddressId)
                .ForeignKey("dbo.Store", t => t.StoreId)
                .Index(t => t.StoreId)
                .Index(t => t.BillingAddressId, name: "IX_BillingAddress_Id")
                .Index(t => t.ShippingAddressId, name: "IX_ShippingAddress_Id");
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 100),
                        Zip = c.String(maxLength: 20),
                        Street2 = c.String(maxLength: 100),
                        StateOther = c.String(),
                        CountryId = c.Int(),
                        ShippingId = c.Int(),
                        StateId = c.Int(),
                        StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .ForeignKey("dbo.OrderShipping", t => t.ShippingId, cascadeDelete: true)
                .ForeignKey("dbo.State", t => t.StateId)
                .ForeignKey("dbo.Store", t => t.StoreId)
                .Index(t => t.CountryId, name: "IX_Country_Id")
                .Index(t => t.ShippingId, name: "IX_Shipping_Id")
                .Index(t => t.StateId, name: "IX_State_Id")
                .Index(t => t.StoreId, name: "IX_Store_Id");
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderShipping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Instructions = c.String(),
                        AddressId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .Index(t => t.OrderId, name: "IX_Order_Id");
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        LastUpdatedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                        Name = c.String(),
                        Logo = c.String(),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 255),
                        TimezoneId = c.String(nullable: false),
                        AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        LastUpdatedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Sku = c.String(nullable: false, maxLength: 20),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Photo = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Store", t => t.StoreId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.OrderPayment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .Index(t => t.OrderId, name: "IX_Order_Id");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "StoreId", "dbo.Store");
            DropForeignKey("dbo.OrderShipping", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderPayment", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Customer", "StoreId", "dbo.Store");
            DropForeignKey("dbo.Customer", "ShippingAddressId", "dbo.Address");
            DropForeignKey("dbo.Customer", "BillingAddressId", "dbo.Address");
            DropForeignKey("dbo.Product", "StoreId", "dbo.Store");
            DropForeignKey("dbo.Address", "StoreId", "dbo.Store");
            DropForeignKey("dbo.Address", "StateId", "dbo.State");
            DropForeignKey("dbo.Address", "ShippingId", "dbo.OrderShipping");
            DropForeignKey("dbo.Address", "CountryId", "dbo.Country");
            DropIndex("dbo.OrderPayment", "IX_Order_Id");
            DropIndex("dbo.Product", new[] { "StoreId" });
            DropIndex("dbo.OrderShipping", "IX_Order_Id");
            DropIndex("dbo.Address", "IX_Store_Id");
            DropIndex("dbo.Address", "IX_State_Id");
            DropIndex("dbo.Address", "IX_Shipping_Id");
            DropIndex("dbo.Address", "IX_Country_Id");
            DropIndex("dbo.Customer", "IX_ShippingAddress_Id");
            DropIndex("dbo.Customer", "IX_BillingAddress_Id");
            DropIndex("dbo.Customer", new[] { "StoreId" });
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropIndex("dbo.Order", new[] { "StoreId" });
            DropIndex("dbo.OrderItem", new[] { "ProductId" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropTable("dbo.OrderPayment");
            DropTable("dbo.Product");
            DropTable("dbo.Store");
            DropTable("dbo.State");
            DropTable("dbo.OrderShipping");
            DropTable("dbo.Country");
            DropTable("dbo.Address");
            DropTable("dbo.Customer");
            DropTable("dbo.Order");
            DropTable("dbo.OrderItem");
        }
    }
}
