namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Art : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SelectedNews", "NewsId", "dbo.News");
            DropIndex("dbo.SelectedNews", new[] { "NewsId" });
            AddColumn("dbo.SelectedNews", "ArticleId", c => c.Int(nullable: false));
            CreateIndex("dbo.SelectedNews", "ArticleId");
            AddForeignKey("dbo.SelectedNews", "ArticleId", "dbo.Articles", "ArticleId", cascadeDelete: true);
            DropColumn("dbo.SelectedNews", "NewsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SelectedNews", "NewsId", c => c.Int(nullable: false));
            DropForeignKey("dbo.SelectedNews", "ArticleId", "dbo.Articles");
            DropIndex("dbo.SelectedNews", new[] { "ArticleId" });
            DropColumn("dbo.SelectedNews", "ArticleId");
            CreateIndex("dbo.SelectedNews", "NewsId");
            AddForeignKey("dbo.SelectedNews", "NewsId", "dbo.News", "NewsId", cascadeDelete: true);
        }
    }
}
