namespace Task.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Queries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        WorkerId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        StartedAt = c.DateTime(),
                        EndedAt = c.DateTime(),
                        QueryStatus = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: true)
                .Index(t => t.WorkerId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Password = c.String(),
                        NickName = c.String(),
                        IsBusy = c.Boolean(nullable: false),
                        Position = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Queries", "WorkerId", "dbo.Workers");
            DropIndex("dbo.Queries", new[] { "WorkerId" });
            DropTable("dbo.Workers");
            DropTable("dbo.Queries");
        }
    }
}
