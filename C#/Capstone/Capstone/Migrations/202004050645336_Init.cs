namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Barcode = c.String(),
                        ExpiryDate = c.DateTime(),
                        Quantity = c.Double(nullable: false),
                        Kitchen_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kitchens", t => t.Kitchen_Id)
                .Index(t => t.Kitchen_Id);
            
            CreateTable(
                "dbo.Kitchens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        GuiltLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kitchens", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Foods", "Kitchen_Id", "dbo.Kitchens");
            DropIndex("dbo.Kitchens", new[] { "User_Id" });
            DropIndex("dbo.Foods", new[] { "Kitchen_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Kitchens");
            DropTable("dbo.Foods");
        }
    }
}
