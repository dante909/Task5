namespace TaskMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        ManagerID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                    })
                .PrimaryKey(t => t.ManagerID);
            
            CreateTable(
                "dbo.SaleInfo",
                c => new
                    {
                        SaleInfoID = c.Int(nullable: false, identity: true),
                        ManagerID = c.Int(nullable: false),
                        ClientName = c.String(),
                        ProductName = c.String(),
                        ProductCost = c.String(),
                        DateOfSale = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SaleInfoID)
                .ForeignKey("dbo.Manager", t => t.ManagerID, cascadeDelete: true)
                .Index(t => t.ManagerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleInfo", "ManagerID", "dbo.Manager");
            DropIndex("dbo.SaleInfo", new[] { "ManagerID" });
            DropTable("dbo.SaleInfo");
            DropTable("dbo.Manager");
        }
    }
}
