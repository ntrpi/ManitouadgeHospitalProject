namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20210804949amanda : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Alerts", "EventId", "dbo.Events");
            DropForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings");
            DropIndex("dbo.Alerts", new[] { "jobPostingId" });
            DropIndex("dbo.Alerts", new[] { "EventId" });
            AddColumn("dbo.Events", "alertId", c => c.Int());
            AddColumn("dbo.JobPostings", "alertId", c => c.Int());
            CreateIndex("dbo.Events", "alertId");
            CreateIndex("dbo.JobPostings", "alertId");
            AddForeignKey("dbo.Events", "alertId", "dbo.Alerts", "alertId");
            AddForeignKey("dbo.JobPostings", "alertId", "dbo.Alerts", "alertId");
            DropColumn("dbo.Alerts", "jobPostingId");
            DropColumn("dbo.Alerts", "EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Alerts", "EventId", c => c.Int());
            AddColumn("dbo.Alerts", "jobPostingId", c => c.Int());
            DropForeignKey("dbo.JobPostings", "alertId", "dbo.Alerts");
            DropForeignKey("dbo.Events", "alertId", "dbo.Alerts");
            DropIndex("dbo.JobPostings", new[] { "alertId" });
            DropIndex("dbo.Events", new[] { "alertId" });
            DropColumn("dbo.JobPostings", "alertId");
            DropColumn("dbo.Events", "alertId");
            CreateIndex("dbo.Alerts", "EventId");
            CreateIndex("dbo.Alerts", "jobPostingId");
            AddForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings", "jobPostingId");
            AddForeignKey("dbo.Alerts", "EventId", "dbo.Events", "EventId");
        }
    }
}
