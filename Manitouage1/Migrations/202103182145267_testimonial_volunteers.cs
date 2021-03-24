namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testimonial_volunteers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        testimonialId = c.Int(nullable: false, identity: true),
                        creationDate = c.DateTime(nullable: false),
                        testimonial = c.String(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.testimonialId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        volunteerId = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        policeCheckPass = c.Boolean(nullable: false),
                        email = c.String(nullable: false),
                        phone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.volunteerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Testimonials", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Testimonials", new[] { "User_Id" });
            DropTable("dbo.Volunteers");
            DropTable("dbo.Testimonials");
        }
    }
}
