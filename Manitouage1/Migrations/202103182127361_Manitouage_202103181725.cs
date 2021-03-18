namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manitouage_202103181725 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Invoices", "Id");
            AddForeignKey("dbo.Invoices", "Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Invoices", new[] { "Id" });
            DropColumn("dbo.Invoices", "Id");
        }
    }
}
