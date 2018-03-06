namespace Ecommerce01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => new { t.CompanyId, t.Description }, unique: true, name: "Category_CompanyId_Description_Index");
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Phone = c.String(nullable: false, maxLength: 20),
                        AddressO = c.String(nullable: false, maxLength: 200),
                        AddressL = c.String(maxLength: 200),
                        Logo = c.String(),
                        DepartamentId = c.Int(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Locality = c.String(maxLength: 100),
                        PartitaIva = c.String(maxLength: 15),
                        CodiceFiscale = c.String(maxLength: 15),
                        PhoneMobil = c.String(maxLength: 20),
                        Fax = c.String(),
                        Email = c.String(nullable: false),
                        http = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.Departament", t => t.DepartamentId)
                .ForeignKey("dbo.Province", t => t.ProvinceId)
                .Index(t => new { t.DepartamentId, t.Name }, unique: true, name: "Company_Name_DepartamentId_Index")
                .Index(t => t.ProvinceId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SigCap = c.String(nullable: false, maxLength: 5),
                        DepartamentId = c.Int(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        Latitud = c.Decimal(precision: 18, scale: 6),
                        Longitud = c.Decimal(precision: 18, scale: 6),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Province", t => t.ProvinceId)
                .ForeignKey("dbo.Departament", t => t.DepartamentId)
                .Index(t => new { t.DepartamentId, t.Name }, unique: true, name: "City_Name_DepartamentId_Index")
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateBirth = c.DateTime(),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 100),
                        DepartamentId = c.Int(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.Departament", t => t.DepartamentId)
                .ForeignKey("dbo.Province", t => t.ProvinceId)
                .Index(t => t.UserName, unique: true, name: "Customer_UserName_Index")
                .Index(t => t.DepartamentId)
                .Index(t => t.ProvinceId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.CompanyCustomer",
                c => new
                    {
                        CompanyCustomerId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyCustomerId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .Index(t => t.CompanyId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Departament",
                c => new
                    {
                        DepartamentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Latitud = c.Decimal(precision: 18, scale: 6),
                        Longitud = c.Decimal(precision: 18, scale: 6),
                    })
                .PrimaryKey(t => t.DepartamentId)
                .Index(t => t.Name, unique: true, name: "Department_Name_Index");
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        ProvinceId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SigCap = c.String(nullable: false, maxLength: 5),
                        TwoInitial = c.String(nullable: false, maxLength: 2),
                        Latitud = c.Decimal(precision: 18, scale: 6),
                        Longitud = c.Decimal(precision: 18, scale: 6),
                        DepartamentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProvinceId)
                .ForeignKey("dbo.Departament", t => t.DepartamentId)
                .Index(t => new { t.DepartamentId, t.Name }, unique: true, name: "Province_Name_Index");
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateCreated = c.DateTime(nullable: false),
                        IsfavoriteSupplier = c.Boolean(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 100),
                        DepartamentId = c.Int(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierId)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.Departament", t => t.DepartamentId)
                .ForeignKey("dbo.Province", t => t.ProvinceId)
                .Index(t => t.DepartamentId)
                .Index(t => t.ProvinceId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.CompanySupplier",
                c => new
                    {
                        CompanySupplierId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanySupplierId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Supplier", t => t.SupplierId)
                .Index(t => t.CompanyId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateBirth = c.DateTime(),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 100),
                        Photo = c.String(),
                        DepartamentId = c.Int(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Departament", t => t.DepartamentId)
                .ForeignKey("dbo.Province", t => t.ProvinceId)
                .Index(t => t.UserName, unique: true, name: "User_UserName_Index")
                .Index(t => t.DepartamentId)
                .Index(t => t.ProvinceId)
                .Index(t => t.CityId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Warehouse",
                c => new
                    {
                        WarehouseId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 100),
                        DepartamentId = c.Int(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WarehouseId)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Departament", t => t.DepartamentId)
                .ForeignKey("dbo.Province", t => t.ProvinceId)
                .Index(t => new { t.CompanyId, t.Name }, unique: true, name: "Warehouse_CompanyId_Name_Index")
                .Index(t => t.DepartamentId)
                .Index(t => t.ProvinceId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Inventory",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        WarehouseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Stock = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.Warehouse", t => t.WarehouseId)
                .Index(t => new { t.WarehouseId, t.ProductId }, unique: true, name: "Inventory_WarehouseId_ProductId_Index");
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
                        BarCode = c.String(nullable: false, maxLength: 13),
                        VendorPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VendorProductCode = c.String(maxLength: 13),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxId = c.Int(nullable: false),
                        Image = c.String(),
                        Remarks = c.String(),
                        ReorderPoint = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Tax", t => t.TaxId)
                .Index(t => new { t.CompanyId, t.BarCode }, unique: true, name: "Product_CompanyId_BarCode_Index")
                .Index(t => new { t.CompanyId, t.Description }, unique: true, name: "Product_CompanyId_Description_Index")
                .Index(t => t.CategoryId)
                .Index(t => t.TaxId);
            
            CreateTable(
                "dbo.Tax",
                c => new
                    {
                        TaxId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 1),
                        Description = c.String(nullable: false, maxLength: 50),
                        Rate = c.Double(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaxId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => new { t.CompanyId, t.Description }, unique: true, name: "Tax_CompanyId_Description_Index");
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.StateId)
                .Index(t => t.Description, unique: true, name: "State_Description_Index");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.City", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.Warehouse", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Inventory", "WarehouseId", "dbo.Warehouse");
            DropForeignKey("dbo.Product", "TaxId", "dbo.Tax");
            DropForeignKey("dbo.Tax", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Inventory", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Warehouse", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.Warehouse", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Warehouse", "CityId", "dbo.City");
            DropForeignKey("dbo.User", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.User", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.User", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.User", "CityId", "dbo.City");
            DropForeignKey("dbo.Supplier", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Supplier", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.CompanySupplier", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.CompanySupplier", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Supplier", "CityId", "dbo.City");
            DropForeignKey("dbo.Province", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.Customer", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Company", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.City", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Customer", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.Company", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.CompanyCustomer", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.CompanyCustomer", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Customer", "CityId", "dbo.City");
            DropForeignKey("dbo.Company", "CityId", "dbo.City");
            DropForeignKey("dbo.Category", "CompanyId", "dbo.Company");
            DropIndex("dbo.State", "State_Description_Index");
            DropIndex("dbo.Tax", "Tax_CompanyId_Description_Index");
            DropIndex("dbo.Product", new[] { "TaxId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.Product", "Product_CompanyId_Description_Index");
            DropIndex("dbo.Product", "Product_CompanyId_BarCode_Index");
            DropIndex("dbo.Inventory", "Inventory_WarehouseId_ProductId_Index");
            DropIndex("dbo.Warehouse", new[] { "CityId" });
            DropIndex("dbo.Warehouse", new[] { "ProvinceId" });
            DropIndex("dbo.Warehouse", new[] { "DepartamentId" });
            DropIndex("dbo.Warehouse", "Warehouse_CompanyId_Name_Index");
            DropIndex("dbo.User", new[] { "CompanyId" });
            DropIndex("dbo.User", new[] { "CityId" });
            DropIndex("dbo.User", new[] { "ProvinceId" });
            DropIndex("dbo.User", new[] { "DepartamentId" });
            DropIndex("dbo.User", "User_UserName_Index");
            DropIndex("dbo.CompanySupplier", new[] { "SupplierId" });
            DropIndex("dbo.CompanySupplier", new[] { "CompanyId" });
            DropIndex("dbo.Supplier", new[] { "CityId" });
            DropIndex("dbo.Supplier", new[] { "ProvinceId" });
            DropIndex("dbo.Supplier", new[] { "DepartamentId" });
            DropIndex("dbo.Province", "Province_Name_Index");
            DropIndex("dbo.Departament", "Department_Name_Index");
            DropIndex("dbo.CompanyCustomer", new[] { "CustomerId" });
            DropIndex("dbo.CompanyCustomer", new[] { "CompanyId" });
            DropIndex("dbo.Customer", new[] { "CityId" });
            DropIndex("dbo.Customer", new[] { "ProvinceId" });
            DropIndex("dbo.Customer", new[] { "DepartamentId" });
            DropIndex("dbo.Customer", "Customer_UserName_Index");
            DropIndex("dbo.City", new[] { "ProvinceId" });
            DropIndex("dbo.City", "City_Name_DepartamentId_Index");
            DropIndex("dbo.Company", new[] { "CityId" });
            DropIndex("dbo.Company", new[] { "ProvinceId" });
            DropIndex("dbo.Company", "Company_Name_DepartamentId_Index");
            DropIndex("dbo.Category", "Category_CompanyId_Description_Index");
            DropTable("dbo.State");
            DropTable("dbo.Tax");
            DropTable("dbo.Product");
            DropTable("dbo.Inventory");
            DropTable("dbo.Warehouse");
            DropTable("dbo.User");
            DropTable("dbo.CompanySupplier");
            DropTable("dbo.Supplier");
            DropTable("dbo.Province");
            DropTable("dbo.Departament");
            DropTable("dbo.CompanyCustomer");
            DropTable("dbo.Customer");
            DropTable("dbo.City");
            DropTable("dbo.Company");
            DropTable("dbo.Category");
        }
    }
}
