namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202104241953_sandra : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "alertId", "dbo.Alerts");
            DropForeignKey("dbo.JobPostings", "alertId", "dbo.Alerts");
            DropIndex("dbo.Events", new[] { "alertId" });
            DropIndex("dbo.JobPostings", new[] { "alertId" });
            AddColumn("dbo.Alerts", "eventId", c => c.Int());
            AddColumn("dbo.Alerts", "jobPostingId", c => c.Int());
            CreateIndex("dbo.Alerts", "eventId");
            CreateIndex("dbo.Alerts", "jobPostingId");
            AddForeignKey("dbo.Alerts", "eventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings", "jobPostingId");
            DropColumn("dbo.Events", "alertId");
            DropColumn("dbo.Donations", "Title");
            DropColumn("dbo.JobPostings", "alertId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobPostings", "alertId", c => c.Int());
            AddColumn("dbo.Donations", "Title", c => c.String());
            AddColumn("dbo.Events", "alertId", c => c.Int());
            DropForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings");
            DropForeignKey("dbo.Alerts", "eventId", "dbo.Events");
            DropIndex("dbo.Alerts", new[] { "jobPostingId" });
            DropIndex("dbo.Alerts", new[] { "eventId" });
            DropColumn("dbo.Alerts", "jobPostingId");
            DropColumn("dbo.Alerts", "eventId");
            CreateIndex("dbo.JobPostings", "alertId");
            CreateIndex("dbo.Events", "alertId");
            AddForeignKey("dbo.JobPostings", "alertId", "dbo.Alerts", "alertId");
            AddForeignKey("dbo.Events", "alertId", "dbo.Alerts", "alertId");
        }
    }
}
