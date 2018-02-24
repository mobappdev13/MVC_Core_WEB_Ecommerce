namespace Ecommerce01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductsTaxes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
                        BarCode = c.String(nullable: false, maxLength: 13),
                        CategoryId = c.Int(nullable: false),
                        TaxId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Tax", t => t.TaxId)
                .Index(t => new { t.CompanyId, t.BarCode }, unique: true, name: "Product_CompanyId_BarCode_Index")
                .Index(t => new { t.CompanyId, t.Description }, unique: true, name: "Product_CompanyId_Description_Index")
                .Index(t => t.CategoryId)
                .Index(t => t.TaxId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "TaxId", "dbo.Tax");
            DropForeignKey("dbo.Product", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropIndex("dbo.Product", new[] { "TaxId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.Product", "Product_CompanyId_Description_Index");
            DropIndex("dbo.Product", "Product_CompanyId_BarCode_Index");
            DropTable("dbo.Product");
        }
    }
}
