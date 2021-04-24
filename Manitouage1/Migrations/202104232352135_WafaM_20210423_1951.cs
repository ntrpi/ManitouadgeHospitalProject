namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WafaM_20210423_1951 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Donations", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Donations", "Title", c => c.String());
        }
    }
}
