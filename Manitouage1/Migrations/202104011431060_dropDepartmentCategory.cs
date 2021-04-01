namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropDepartmentCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "departmentCategoryId", "dbo.DepartmentCategories");
            DropIndex("dbo.Departments", new[] { "departmentCategoryId" });
            AddColumn("dbo.Departments", "category", c => c.String(nullable: false));
            DropColumn("dbo.Departments", "departmentCategoryId");
            DropColumn("dbo.Departments", "departmentCategoryName");
            DropTable("dbo.DepartmentCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DepartmentCategories",
                c => new
                    {
                        departmentCategoryId = c.Int(nullable: false, identity: true),
                        departmentCategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.departmentCategoryId);
            
            AddColumn("dbo.Departments", "departmentCategoryName", c => c.String());
            AddColumn("dbo.Departments", "departmentCategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.Departments", "category");
            CreateIndex("dbo.Departments", "departmentCategoryId");
            AddForeignKey("dbo.Departments", "departmentCategoryId", "dbo.DepartmentCategories", "departmentCategoryId", cascadeDelete: true);
        }
    }
}
