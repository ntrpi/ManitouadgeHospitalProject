namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manitouage_202103181634 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductXInvoices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                        invoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Invoices", t => t.invoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .Index(t => t.productId)
                .Index(t => t.invoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductXInvoices", "productId", "dbo.Products");
            DropForeignKey("dbo.ProductXInvoices", "invoiceId", "dbo.Invoices");
            DropIndex("dbo.ProductXInvoices", new[] { "invoiceId" });
            DropIndex("dbo.ProductXInvoices", new[] { "productId" });
            DropTable("dbo.ProductXInvoices");
        }
    }
}
