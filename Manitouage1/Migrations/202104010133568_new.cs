namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        alertId = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        dateTime = c.DateTime(nullable: false),
                        description = c.String(nullable: false),
                        jobPostingId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.alertId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.JobPostings", t => t.jobPostingId, cascadeDelete: true)
                .Index(t => t.jobPostingId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Location = c.String(nullable: false),
                        Duration = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContactPerson = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            AddColumn("dbo.Invoices", "issued", c => c.DateTime());
            AddColumn("dbo.Invoices", "paid", c => c.DateTime());
            AddColumn("dbo.Invoices", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings");
            DropForeignKey("dbo.Alerts", "EventId", "dbo.Events");
            DropIndex("dbo.Alerts", new[] { "EventId" });
            DropIndex("dbo.Alerts", new[] { "jobPostingId" });
            DropColumn("dbo.Invoices", "status");
            DropColumn("dbo.Invoices", "paid");
            DropColumn("dbo.Invoices", "issued");
            DropTable("dbo.Events");
            DropTable("dbo.Alerts");
        }
    }
}
