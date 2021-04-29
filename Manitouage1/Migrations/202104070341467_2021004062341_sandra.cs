namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2021004062341_sandra : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Alerts", "EventId", "dbo.Events");
            DropForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings");
            DropIndex("dbo.Alerts", new[] { "jobPostingId" });
            DropIndex("dbo.Alerts", new[] { "EventId" });
            AddColumn("dbo.Donations", "EventId", c => c.Int(nullable: false));
            AlterColumn("dbo.Alerts", "jobPostingId", c => c.Int());
            AlterColumn("dbo.Alerts", "EventId", c => c.Int());
            CreateIndex("dbo.Alerts", "jobPostingId");
            CreateIndex("dbo.Alerts", "EventId");
            CreateIndex("dbo.Donations", "EventId");
            AddForeignKey("dbo.Donations", "EventId", "dbo.Events", "EventId", cascadeDelete: true);
            AddForeignKey("dbo.Alerts", "EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings", "jobPostingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings");
            DropForeignKey("dbo.Alerts", "EventId", "dbo.Events");
            DropForeignKey("dbo.Donations", "EventId", "dbo.Events");
            DropIndex("dbo.Donations", new[] { "EventId" });
            DropIndex("dbo.Alerts", new[] { "EventId" });
            DropIndex("dbo.Alerts", new[] { "jobPostingId" });
            AlterColumn("dbo.Alerts", "EventId", c => c.Int(nullable: false));
            AlterColumn("dbo.Alerts", "jobPostingId", c => c.Int(nullable: false));
            DropColumn("dbo.Donations", "EventId");
            CreateIndex("dbo.Alerts", "EventId");
            CreateIndex("dbo.Alerts", "jobPostingId");
            AddForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings", "jobPostingId", cascadeDelete: true);
            AddForeignKey("dbo.Alerts", "EventId", "dbo.Events", "EventId", cascadeDelete: true);
        }
    }
}
