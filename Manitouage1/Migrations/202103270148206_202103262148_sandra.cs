namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202103262148_sandra : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Location = c.String(),
                        Duration = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContactPerson = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            DropTable("dbo.InvoiceDtoes");
        }
        
        public override void Down()
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
            
            DropTable("dbo.Events");
        }
    }
}
