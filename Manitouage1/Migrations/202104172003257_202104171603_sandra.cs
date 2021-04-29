namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202104171603_sandra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductXInvoices", "quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductXInvoices", "quantity");
        }
    }
}
