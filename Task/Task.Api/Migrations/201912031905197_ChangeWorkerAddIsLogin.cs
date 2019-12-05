namespace Task.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeWorkerAddIsLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workers", "IsLogin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workers", "IsLogin");
        }
    }
}
