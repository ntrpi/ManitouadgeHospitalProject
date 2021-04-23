namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WafaM_20210422_1918 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donations", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Donations", "Title");
        }
    }
}
