namespace MovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        BillingAddress = c.String(nullable: false, maxLength: 50),
                        BillingZip = c.String(nullable: false),
                        BillingCity = c.String(nullable: false, maxLength: 50),
                        DeliveryAddress = c.String(nullable: false, maxLength: 50),
                        DeliveryZip = c.String(nullable: false),
                        DeliveryCity = c.String(nullable: false, maxLength: 50),
                        EmailAddress = c.String(nullable: false),
                        PhoneNo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Director = c.String(nullable: false, maxLength: 100),
                        ReleaseYear = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderRows",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderID = c.Int(nullable: false),
                        MovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movies", t => t.MovieID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.MovieID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderRows", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.OrderRows", "MovieID", "dbo.Movies");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropIndex("dbo.OrderRows", new[] { "MovieID" });
            DropIndex("dbo.OrderRows", new[] { "OrderID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderRows");
            DropTable("dbo.Movies");
            DropTable("dbo.Customers");
        }
    }
}
