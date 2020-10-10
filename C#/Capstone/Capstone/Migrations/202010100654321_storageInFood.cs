namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storageInFood : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Foods", "StorageType_Id", c => c.Int());
            CreateIndex("dbo.Foods", "StorageType_Id");
            AddForeignKey("dbo.Foods", "StorageType_Id", "dbo.Storages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Foods", "StorageType_Id", "dbo.Storages");
            DropIndex("dbo.Foods", new[] { "StorageType_Id" });
            DropColumn("dbo.Foods", "StorageType_Id");
        }
    }
}
