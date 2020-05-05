namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SelectedNews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SelectedNews",
                c => new
                    {
                        SelectedNewsId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SelectedNewsId)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SelectedNews", "NewsId", "dbo.News");
            DropIndex("dbo.SelectedNews", new[] { "NewsId" });
            DropTable("dbo.SelectedNews");
        }
    }
}
