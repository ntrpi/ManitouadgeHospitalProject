namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202103262207_sandra : DbMigration
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
                    })
                .PrimaryKey(t => t.alertId);
            
            AlterColumn("dbo.Events", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "ContactPerson", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "ContactPerson", c => c.String());
            AlterColumn("dbo.Events", "Location", c => c.String());
            AlterColumn("dbo.Events", "Description", c => c.String());
            AlterColumn("dbo.Events", "Title", c => c.String());
            DropTable("dbo.Alerts");
        }
    }
}
