namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDepartment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentCategories",
                c => new
                    {
                        departmentCategoryId = c.Int(nullable: false, identity: true),
                        departmentCategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.departmentCategoryId);
            
            AddColumn("dbo.Departments", "departmentCategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Departments", "departmentCategoryName", c => c.String());
            CreateIndex("dbo.Departments", "departmentCategoryId");
            AddForeignKey("dbo.Departments", "departmentCategoryId", "dbo.DepartmentCategories", "departmentCategoryId", cascadeDelete: true);
            DropColumn("dbo.Departments", "category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "category", c => c.String(nullable: false));
            DropForeignKey("dbo.Departments", "departmentCategoryId", "dbo.DepartmentCategories");
            DropIndex("dbo.Departments", new[] { "departmentCategoryId" });
            DropColumn("dbo.Departments", "departmentCategoryName");
            DropColumn("dbo.Departments", "departmentCategoryId");
            DropTable("dbo.DepartmentCategories");
        }
    }
}
