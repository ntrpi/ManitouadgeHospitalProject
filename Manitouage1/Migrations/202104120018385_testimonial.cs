namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testimonial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Testimonials", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Testimonials", new[] { "UserId" });
            AlterColumn("dbo.Testimonials", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Testimonials", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Testimonials", "UserId");
            AddForeignKey("dbo.Testimonials", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
