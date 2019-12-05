namespace Task.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeQueryEntityModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Queries", "WorkerId", "dbo.Workers");
            DropIndex("dbo.Queries", new[] { "WorkerId" });
            AlterColumn("dbo.Queries", "WorkerId", c => c.Int());
            CreateIndex("dbo.Queries", "WorkerId");
            AddForeignKey("dbo.Queries", "WorkerId", "dbo.Workers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Queries", "WorkerId", "dbo.Workers");
            DropIndex("dbo.Queries", new[] { "WorkerId" });
            AlterColumn("dbo.Queries", "WorkerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Queries", "WorkerId");
            AddForeignKey("dbo.Queries", "WorkerId", "dbo.Workers", "Id", cascadeDelete: true);
        }
    }
}
