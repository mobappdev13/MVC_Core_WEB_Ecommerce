namespace Ecommerce01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => new { t.CompanyId, t.Description }, unique: true, name: "Category_CompanyId_Description_Index")
                .Index(t => t.User_UserId);
            
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
                .Index(t => new { t.DepartamentId, t.Name }, unique: true, name: "City_Name_Index")
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
                .Index(t => new { t.DepartamentId, t.Name }, unique: true, name: "City_Name_Index")
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Customers",
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
                .Index(t => t.DepartamentId)
                .Index(t => t.ProvinceId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.CompanyCustomers",
                c => new
                    {
                        CompanyCustomerId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyCustomerId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
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
                "dbo.Users",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.City", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.Users", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Users", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.Users", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Users", "CityId", "dbo.City");
            DropForeignKey("dbo.Category", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Province", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.Customers", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Company", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.City", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Customers", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.Company", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.CompanyCustomers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CompanyCustomers", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Customers", "CityId", "dbo.City");
            DropForeignKey("dbo.Company", "CityId", "dbo.City");
            DropForeignKey("dbo.Category", "CompanyId", "dbo.Company");
            DropIndex("dbo.Users", new[] { "CompanyId" });
            DropIndex("dbo.Users", new[] { "CityId" });
            DropIndex("dbo.Users", new[] { "ProvinceId" });
            DropIndex("dbo.Users", new[] { "DepartamentId" });
            DropIndex("dbo.Users", "User_UserName_Index");
            DropIndex("dbo.Province", "Province_Name_Index");
            DropIndex("dbo.Departament", "Department_Name_Index");
            DropIndex("dbo.CompanyCustomers", new[] { "CustomerId" });
            DropIndex("dbo.CompanyCustomers", new[] { "CompanyId" });
            DropIndex("dbo.Customers", new[] { "CityId" });
            DropIndex("dbo.Customers", new[] { "ProvinceId" });
            DropIndex("dbo.Customers", new[] { "DepartamentId" });
            DropIndex("dbo.City", new[] { "ProvinceId" });
            DropIndex("dbo.City", "City_Name_Index");
            DropIndex("dbo.Company", new[] { "CityId" });
            DropIndex("dbo.Company", new[] { "ProvinceId" });
            DropIndex("dbo.Company", "City_Name_Index");
            DropIndex("dbo.Category", new[] { "User_UserId" });
            DropIndex("dbo.Category", "Category_CompanyId_Description_Index");
            DropTable("dbo.Users");
            DropTable("dbo.Province");
            DropTable("dbo.Departament");
            DropTable("dbo.CompanyCustomers");
            DropTable("dbo.Customers");
            DropTable("dbo.City");
            DropTable("dbo.Company");
            DropTable("dbo.Category");
        }
    }
}
