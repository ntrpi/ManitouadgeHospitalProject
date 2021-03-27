namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20212603214100 : DbMigration
    {
        public override void Up()
        {
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
        }
    }
}
