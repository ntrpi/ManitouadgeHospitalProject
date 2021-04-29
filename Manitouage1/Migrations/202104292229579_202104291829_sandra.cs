namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202104291829_sandra : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Volunteers", "survey", c => c.String(nullable: false));
            AddColumn("dbo.Volunteers", "EventId", c => c.Int());
            CreateIndex("dbo.Volunteers", "EventId");
            AddForeignKey("dbo.Volunteers", "EventId", "dbo.Events", "EventId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Volunteers", "EventId", "dbo.Events");
            DropIndex("dbo.Volunteers", new[] { "EventId" });
            DropColumn("dbo.Volunteers", "EventId");
            DropColumn("dbo.Volunteers", "survey");
        }
    }
}
