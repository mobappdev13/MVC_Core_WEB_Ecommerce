namespace Ecommerce01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Warehouses : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Warehouse", "ProvinceId", "dbo.Province");
            DropForeignKey("dbo.Warehouse", "DepartamentId", "dbo.Departament");
            DropForeignKey("dbo.Warehouse", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Warehouse", "CityId", "dbo.City");
            DropIndex("dbo.Warehouse", new[] { "CityId" });
            DropIndex("dbo.Warehouse", new[] { "ProvinceId" });
            DropIndex("dbo.Warehouse", new[] { "DepartamentId" });
            DropIndex("dbo.Warehouse", "Warehouse_CompanyId_Name_Index");
            DropTable("dbo.Warehouse");
        }
    }
}
