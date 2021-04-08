namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04082021amanda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        alertId = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        dateTime = c.DateTime(nullable: false),
                        description = c.String(nullable: false),
                        jobPostingId = c.Int(),
                        EventId = c.Int(),
                    })
                .PrimaryKey(t => t.alertId)
                .ForeignKey("dbo.Events", t => t.EventId)
                .ForeignKey("dbo.JobPostings", t => t.jobPostingId)
                .Index(t => t.jobPostingId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Location = c.String(nullable: false),
                        Duration = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContactPerson = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        donationId = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        email = c.String(nullable: false),
                        phoneNumber = c.Int(nullable: false),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.donationId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.JobPostings",
                c => new
                    {
                        jobPostingId = c.Int(nullable: false, identity: true),
                        jobNumber = c.String(nullable: false),
                        jobTitle = c.String(nullable: false),
                        jobType = c.String(nullable: false),
                        jobDescription = c.String(nullable: false),
                        salary = c.String(nullable: false),
                        closingDate = c.DateTime(nullable: false),
                        departmentId = c.Int(nullable: false),
                        departmentName = c.String(),
                    })
                .PrimaryKey(t => t.jobPostingId)
                .ForeignKey("dbo.Departments", t => t.departmentId, cascadeDelete: true)
                .Index(t => t.departmentId);
            
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
                        issued = c.DateTime(),
                        paid = c.DateTime(),
                        status = c.Int(nullable: false),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.invoiceId)
                .ForeignKey("dbo.AspNetUsers", t => t.userId)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProductXInvoices", "productId", "dbo.Products");
            DropForeignKey("dbo.ProductXInvoices", "invoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Alerts", "jobPostingId", "dbo.JobPostings");
            DropForeignKey("dbo.JobPostings", "departmentId", "dbo.Departments");
            DropForeignKey("dbo.Alerts", "EventId", "dbo.Events");
            DropForeignKey("dbo.Donations", "EventId", "dbo.Events");
            DropIndex("dbo.Testimonials", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProductXInvoices", new[] { "invoiceId" });
            DropIndex("dbo.ProductXInvoices", new[] { "productId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Invoices", new[] { "userId" });
            DropIndex("dbo.JobPostings", new[] { "departmentId" });
            DropIndex("dbo.Donations", new[] { "EventId" });
            DropIndex("dbo.Alerts", new[] { "EventId" });
            DropIndex("dbo.Alerts", new[] { "jobPostingId" });
            DropTable("dbo.Volunteers");
            DropTable("dbo.Testimonials");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Products");
            DropTable("dbo.ProductXInvoices");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Invoices");
            DropTable("dbo.Departments");
            DropTable("dbo.JobPostings");
            DropTable("dbo.Donations");
            DropTable("dbo.Events");
            DropTable("dbo.Alerts");
        }
    }
}
