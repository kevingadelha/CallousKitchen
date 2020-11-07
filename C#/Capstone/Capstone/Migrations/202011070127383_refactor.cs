namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foods", "StorageId", "dbo.Storages");
            DropIndex("dbo.Foods", new[] { "StorageId" });
            AddColumn("dbo.Foods", "Storage", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "CalculatedExpiryDate", c => c.DateTime());
            AddColumn("dbo.Foods", "QuantityClassifier", c => c.String());
            AddColumn("dbo.Foods", "Favourite", c => c.Boolean(nullable: false));
            DropColumn("dbo.Foods", "Barcode");
            DropColumn("dbo.Foods", "StorageId");
            DropColumn("dbo.Users", "Username");
            DropColumn("dbo.Users", "GuiltLevel");
            DropTable("dbo.Storages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "GuiltLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Username", c => c.String());
            AddColumn("dbo.Foods", "StorageId", c => c.Int(nullable: false));
            AddColumn("dbo.Foods", "Barcode", c => c.String());
            DropColumn("dbo.Foods", "Favourite");
            DropColumn("dbo.Foods", "QuantityClassifier");
            DropColumn("dbo.Foods", "CalculatedExpiryDate");
            DropColumn("dbo.Foods", "Storage");
            CreateIndex("dbo.Foods", "StorageId");
            AddForeignKey("dbo.Foods", "StorageId", "dbo.Storages", "Id", cascadeDelete: true);
        }
    }
}
