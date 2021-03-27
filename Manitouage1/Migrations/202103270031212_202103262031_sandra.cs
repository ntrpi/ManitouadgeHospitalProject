namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202103262031_sandra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "issued", c => c.DateTime());
            AddColumn("dbo.Invoices", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "status");
            DropColumn("dbo.Invoices", "issued");
        }
    }
}
