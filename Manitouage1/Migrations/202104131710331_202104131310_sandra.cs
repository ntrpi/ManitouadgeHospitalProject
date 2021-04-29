namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202104131310_sandra : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Alerts", "EventId", "dbo.Events");
            DropForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings");
            DropForeignKey("dbo.Testimonials", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Alerts", new[] { "jobPostingId" });
            DropIndex("dbo.Alerts", new[] { "EventId" });
            DropIndex("dbo.Testimonials", new[] { "UserId" });
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        ContactUsId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        Reply = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ContactUsId);
            
            AddColumn("dbo.Events", "alertId", c => c.Int());
            AddColumn("dbo.JobPostings", "alertId", c => c.Int());
            AlterColumn("dbo.Testimonials", "UserId", c => c.String());
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
            AlterColumn("dbo.Testimonials", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.JobPostings", "alertId");
            DropColumn("dbo.Events", "alertId");
            DropTable("dbo.ContactUs");
            CreateIndex("dbo.Testimonials", "UserId");
            CreateIndex("dbo.Alerts", "EventId");
            CreateIndex("dbo.Alerts", "jobPostingId");
            AddForeignKey("dbo.Testimonials", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings", "jobPostingId");
            AddForeignKey("dbo.Alerts", "EventId", "dbo.Events", "EventId");
        }
    }
}
