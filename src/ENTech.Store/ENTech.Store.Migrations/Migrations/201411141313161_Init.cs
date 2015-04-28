namespace ENTech.Store.Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 100),
                        Zip = c.String(maxLength: 20),
                        Region = c.String(),
                        Lat = c.Decimal(precision: 18, scale: 2),
                        Lon = c.Decimal(precision: 18, scale: 2),
                        Street2 = c.String(maxLength: 100),
                        StateId = c.Int(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.State", t => t.StateId)
                .Index(t => t.StateId)
                .Index(t => t.CountryId);
            
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
                "dbo.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Code = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BillableItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sku = c.String(nullable: false, maxLength: 20),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductVariantOptionValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductVariantId = c.Int(nullable: false),
                        ProductOptionId = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductOption", t => t.ProductOptionId, cascadeDelete: true)
                .ForeignKey("dbo.ProductVariant", t => t.ProductVariantId)
                .Index(t => t.ProductVariantId)
                .Index(t => t.ProductOptionId);
            
            CreateTable(
                "dbo.ProductOption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        DeletedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategory", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LogoUrl = c.String(),
                        AddressId = c.Int(),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 255),
                        TimezoneId = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        DeletedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.ProductPhoto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false),
                        Url = c.String(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.ServiceSubCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceCategory", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 255),
                        Phone = c.String(maxLength: 20),
                        StoreId = c.Int(nullable: false),
                        UserId = c.Int(),
                        BillingAddressId = c.Int(),
                        ShippingAddressId = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.BillingAddressId)
                .ForeignKey("dbo.Address", t => t.ShippingAddressId)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.StoreId)
                .Index(t => t.UserId)
                .Index(t => t.BillingAddressId)
                .Index(t => t.ShippingAddressId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.Guid(nullable: false),
                        LastLoginTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(maxLength: 128),
                        TypeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserType", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.UserType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(nullable: false),
                        BillableItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillableItem", t => t.BillableItemId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.BillableItemId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StoreId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        PaymentId = c.Int(),
                        ShippingId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Payment", t => t.PaymentId)
                .ForeignKey("dbo.OrderShipping", t => t.ShippingId)
                .ForeignKey("dbo.OrderStatus", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: false)
                .Index(t => t.StoreId)
                .Index(t => t.CustomerId)
                .Index(t => t.StatusId)
                .Index(t => t.PaymentId)
                .Index(t => t.ShippingId);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefundedAt = c.DateTime(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderShipping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Instructions = c.String(),
                        AddressId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.OrderShippingStatus", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.AddressId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.OrderShippingStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Duration = c.Int(nullable: false),
                        Description = c.String(maxLength: 4000),
                        StoreId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        SubCategoryId = c.Int(),
                        DeletedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillableItem", t => t.Id)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceCategory", t => t.CategoryId, cascadeDelete: false)
                .ForeignKey("dbo.ServiceSubCategory", t => t.SubCategoryId)
                .Index(t => t.Id)
                .Index(t => t.StoreId)
                .Index(t => t.CategoryId)
                .Index(t => t.SubCategoryId);
            
            CreateTable(
                "dbo.ProductVariant",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        QuantityInStock = c.Int(nullable: false),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weight = c.Single(nullable: false),
                        ProductId = c.Int(nullable: false),
                        PhotoId = c.Int(),
                        StatusId = c.Int(nullable: false),
                        DeletedAt = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillableItem", t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductPhoto", t => t.PhotoId)
                .ForeignKey("dbo.ProductStatus", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ProductId)
                .Index(t => t.PhotoId)
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductVariant", "StatusId", "dbo.ProductStatus");
            DropForeignKey("dbo.ProductVariant", "PhotoId", "dbo.ProductPhoto");
            DropForeignKey("dbo.ProductVariant", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductVariant", "Id", "dbo.BillableItem");
            DropForeignKey("dbo.Service", "SubCategoryId", "dbo.ServiceSubCategory");
            DropForeignKey("dbo.Service", "CategoryId", "dbo.ServiceCategory");
            DropForeignKey("dbo.Service", "StoreId", "dbo.Store");
            DropForeignKey("dbo.Service", "Id", "dbo.BillableItem");
            DropForeignKey("dbo.Order", "StoreId", "dbo.Store");
            DropForeignKey("dbo.Order", "StatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.Order", "ShippingId", "dbo.OrderShipping");
            DropForeignKey("dbo.OrderShipping", "StatusId", "dbo.OrderShippingStatus");
            DropForeignKey("dbo.OrderShipping", "AddressId", "dbo.Address");
            DropForeignKey("dbo.Order", "PaymentId", "dbo.Payment");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.OrderItem", "BillableItemId", "dbo.BillableItem");
            DropForeignKey("dbo.Customer", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "TypeId", "dbo.UserType");
            DropForeignKey("dbo.Customer", "StoreId", "dbo.Store");
            DropForeignKey("dbo.Customer", "ShippingAddressId", "dbo.Address");
            DropForeignKey("dbo.Customer", "BillingAddressId", "dbo.Address");
            DropForeignKey("dbo.ServiceSubCategory", "CategoryId", "dbo.ServiceCategory");
            DropForeignKey("dbo.ServiceCategory", "StoreId", "dbo.Store");
            DropForeignKey("dbo.ProductVariantOptionValue", "ProductVariantId", "dbo.ProductVariant");
            DropForeignKey("dbo.ProductVariantOptionValue", "ProductOptionId", "dbo.ProductOption");
            DropForeignKey("dbo.ProductOption", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductPhoto", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.ProductCategory", "StoreId", "dbo.Store");
            DropForeignKey("dbo.Store", "AddressId", "dbo.Address");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.ProductCategory");
            DropForeignKey("dbo.Address", "StateId", "dbo.State");
            DropForeignKey("dbo.Address", "CountryId", "dbo.Country");
            DropIndex("dbo.ProductVariant", new[] { "StatusId" });
            DropIndex("dbo.ProductVariant", new[] { "PhotoId" });
            DropIndex("dbo.ProductVariant", new[] { "ProductId" });
            DropIndex("dbo.ProductVariant", new[] { "Id" });
            DropIndex("dbo.Service", new[] { "SubCategoryId" });
            DropIndex("dbo.Service", new[] { "CategoryId" });
            DropIndex("dbo.Service", new[] { "StoreId" });
            DropIndex("dbo.Service", new[] { "Id" });
            DropIndex("dbo.OrderShipping", new[] { "StatusId" });
            DropIndex("dbo.OrderShipping", new[] { "AddressId" });
            DropIndex("dbo.Order", new[] { "ShippingId" });
            DropIndex("dbo.Order", new[] { "PaymentId" });
            DropIndex("dbo.Order", new[] { "StatusId" });
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropIndex("dbo.Order", new[] { "StoreId" });
            DropIndex("dbo.OrderItem", new[] { "BillableItemId" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.User", new[] { "TypeId" });
            DropIndex("dbo.Customer", new[] { "ShippingAddressId" });
            DropIndex("dbo.Customer", new[] { "BillingAddressId" });
            DropIndex("dbo.Customer", new[] { "UserId" });
            DropIndex("dbo.Customer", new[] { "StoreId" });
            DropIndex("dbo.ServiceSubCategory", new[] { "CategoryId" });
            DropIndex("dbo.ServiceCategory", new[] { "StoreId" });
            DropIndex("dbo.ProductPhoto", new[] { "Product_Id" });
            DropIndex("dbo.Store", new[] { "AddressId" });
            DropIndex("dbo.ProductCategory", new[] { "StoreId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.ProductOption", new[] { "ProductId" });
            DropIndex("dbo.ProductVariantOptionValue", new[] { "ProductOptionId" });
            DropIndex("dbo.ProductVariantOptionValue", new[] { "ProductVariantId" });
            DropIndex("dbo.Address", new[] { "CountryId" });
            DropIndex("dbo.Address", new[] { "StateId" });
            DropTable("dbo.ProductVariant");
            DropTable("dbo.Service");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.OrderShippingStatus");
            DropTable("dbo.OrderShipping");
            DropTable("dbo.Payment");
            DropTable("dbo.Order");
            DropTable("dbo.OrderItem");
            DropTable("dbo.UserType");
            DropTable("dbo.User");
            DropTable("dbo.Customer");
            DropTable("dbo.ServiceSubCategory");
            DropTable("dbo.ServiceCategory");
            DropTable("dbo.ProductStatus");
            DropTable("dbo.ProductPhoto");
            DropTable("dbo.Store");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.Product");
            DropTable("dbo.ProductOption");
            DropTable("dbo.ProductVariantOptionValue");
            DropTable("dbo.BillableItem");
            DropTable("dbo.State");
            DropTable("dbo.Country");
            DropTable("dbo.Address");
        }
    }
}
