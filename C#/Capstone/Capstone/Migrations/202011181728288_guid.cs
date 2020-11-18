namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "EmailConfirmKey", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "IsConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsConfirmed");
            DropColumn("dbo.Users", "EmailConfirmKey");
        }
    }
}
