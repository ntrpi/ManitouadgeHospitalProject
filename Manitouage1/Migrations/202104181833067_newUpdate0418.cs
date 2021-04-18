namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newUpdate0418 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "userId", "dbo.AspNetUsers");
            DropIndex("dbo.Invoices", new[] { "userId" });
            AddColumn("dbo.ProductXInvoices", "quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Invoices", "userId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Invoices", "userId");
            AddForeignKey("dbo.Invoices", "userId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "userId", "dbo.AspNetUsers");
            DropIndex("dbo.Invoices", new[] { "userId" });
            AlterColumn("dbo.Invoices", "userId", c => c.String(maxLength: 128));
            DropColumn("dbo.ProductXInvoices", "quantity");
            CreateIndex("dbo.Invoices", "userId");
            AddForeignKey("dbo.Invoices", "userId", "dbo.AspNetUsers", "Id");
        }
    }
}
