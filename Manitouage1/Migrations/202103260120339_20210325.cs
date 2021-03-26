namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20210325 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        departmentId = c.Int(nullable: false, identity: true),
                        departmentName = c.String(nullable: false),
                        email = c.String(nullable: false),
                        phone = c.String(nullable: false),
                        fax = c.String(),
                        extension = c.String(nullable: false),
                        category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.departmentId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        invoiceId = c.Int(nullable: false, identity: true),
                        created = c.DateTime(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.invoiceId)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ProductXInvoices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        productId = c.Int(nullable: false),
                        invoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Invoices", t => t.invoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .Index(t => t.productId)
                .Index(t => t.invoiceId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        productId = c.Int(nullable: false, identity: true),
                        productName = c.String(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        taxRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.productId);
            
            CreateTable(
                "dbo.JobPostings",
                c => new
                    {
                        jobPostingId = c.Int(nullable: false, identity: true),
                        jonNumber = c.String(nullable: false),
                        jobTitle = c.String(nullable: false),
                        jobType = c.String(nullable: false),
                        jobDescription = c.String(nullable: false),
                        closingDate = c.DateTime(nullable: false),
                        departmentId = c.Int(nullable: false),
                        departmentName = c.String(),
                    })
                .PrimaryKey(t => t.jobPostingId)
                .ForeignKey("dbo.Departments", t => t.departmentId, cascadeDelete: true)
                .Index(t => t.departmentId);
            
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        testimonialId = c.Int(nullable: false, identity: true),
                        creationDate = c.DateTime(nullable: false),
                        testimonial = c.String(nullable: false),
                        approved = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.testimonialId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
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
                        approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.volunteerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Testimonials", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPostings", "departmentId", "dbo.Departments");
            DropForeignKey("dbo.ProductXInvoices", "productId", "dbo.Products");
            DropForeignKey("dbo.ProductXInvoices", "invoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Testimonials", new[] { "UserId" });
            DropIndex("dbo.JobPostings", new[] { "departmentId" });
            DropIndex("dbo.ProductXInvoices", new[] { "invoiceId" });
            DropIndex("dbo.ProductXInvoices", new[] { "productId" });
            DropIndex("dbo.Invoices", new[] { "Id" });
            DropTable("dbo.Volunteers");
            DropTable("dbo.Testimonials");
            DropTable("dbo.JobPostings");
            DropTable("dbo.Products");
            DropTable("dbo.ProductXInvoices");
            DropTable("dbo.Invoices");
            DropTable("dbo.Departments");
        }
    }
}
