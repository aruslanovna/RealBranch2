namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "UserId");
        }
    }
}
