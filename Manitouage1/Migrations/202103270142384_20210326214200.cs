namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20210326214200 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings");
            DropForeignKey("dbo.Alerts", "EventId", "dbo.Events");
            DropIndex("dbo.Alerts", new[] { "EventId" });
            DropIndex("dbo.Alerts", new[] { "jobPostingId" });
            DropTable("dbo.Alerts");
        }
    }
}
