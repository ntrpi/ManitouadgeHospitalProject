namespace Manitouage1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manitouage_202103181619 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
