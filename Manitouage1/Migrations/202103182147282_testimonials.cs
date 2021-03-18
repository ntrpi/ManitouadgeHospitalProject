namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testimonials : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Testimonials", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Testimonials", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Testimonials", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Testimonials", name: "UserId", newName: "User_Id");
        }
    }
}
