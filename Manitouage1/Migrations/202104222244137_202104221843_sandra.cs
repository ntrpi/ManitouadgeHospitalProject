namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202104221843_sandra : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Donations", "EventId", "dbo.Events");
            DropIndex("dbo.Donations", new[] { "EventId" });
            AlterColumn("dbo.Donations", "EventId", c => c.Int());
            CreateIndex("dbo.Donations", "EventId");
            AddForeignKey("dbo.Donations", "EventId", "dbo.Events", "EventId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Donations", "EventId", "dbo.Events");
            DropIndex("dbo.Donations", new[] { "EventId" });
            AlterColumn("dbo.Donations", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.Donations", "EventId");
            AddForeignKey("dbo.Donations", "EventId", "dbo.Events", "EventId", cascadeDelete: true);
        }
    }
}
