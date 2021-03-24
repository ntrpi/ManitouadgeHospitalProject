namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testimonial_model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Testimonials", "approved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Testimonials", "approved");
        }
    }
}
