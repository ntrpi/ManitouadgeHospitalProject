namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manitouage_202103181629 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        invoiceId = c.Int(nullable: false, identity: true),
                        created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.invoiceId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Invoices");
        }
    }
}
