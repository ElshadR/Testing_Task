namespace Task.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Queries", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Queries", "UserId");
            AddForeignKey("dbo.Queries", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Queries", "UserId", "dbo.Users");
            DropIndex("dbo.Queries", new[] { "UserId" });
            DropColumn("dbo.Queries", "UserId");
            DropTable("dbo.Users");
        }
    }
}
