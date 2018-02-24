namespace Ecommerce01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Taxes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tax",
                c => new
                    {
                        TaxId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        Rate = c.Double(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaxId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => new { t.CompanyId, t.Description }, unique: true, name: "Tax_CompanyId_Description_Index");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tax", "CompanyId", "dbo.Company");
            DropIndex("dbo.Tax", "Tax_CompanyId_Description_Index");
            DropTable("dbo.Tax");
        }
    }
}
