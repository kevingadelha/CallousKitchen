namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storageWithIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Foods", "StorageType_Id", "dbo.Storages");
            DropIndex("dbo.Foods", new[] { "StorageType_Id" });
            RenameColumn(table: "dbo.Foods", name: "StorageType_Id", newName: "StorageId");
            AlterColumn("dbo.Foods", "StorageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Foods", "StorageId");
            AddForeignKey("dbo.Foods", "StorageId", "dbo.Storages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Foods", "StorageId", "dbo.Storages");
            DropIndex("dbo.Foods", new[] { "StorageId" });
            AlterColumn("dbo.Foods", "StorageId", c => c.Int());
            RenameColumn(table: "dbo.Foods", name: "StorageId", newName: "StorageType_Id");
            CreateIndex("dbo.Foods", "StorageType_Id");
            AddForeignKey("dbo.Foods", "StorageType_Id", "dbo.Storages", "Id");
        }
    }
}
