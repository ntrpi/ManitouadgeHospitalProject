namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202103262057_sandra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceDtoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        invoiceId = c.Int(nullable: false),
                        created = c.DateTime(nullable: false),
                        issued = c.DateTime(),
                        paid = c.DateTime(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Invoices", "paid", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "paid");
            DropTable("dbo.InvoiceDtoes");
        }
    }
}
