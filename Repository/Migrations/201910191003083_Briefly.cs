namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Briefly : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Briefly", c => c.String());
            AddColumn("dbo.Articles", "PostDate", c => c.DateTime());
            AddColumn("dbo.Comments", "PostDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "PostDate");
            DropColumn("dbo.Articles", "PostDate");
            DropColumn("dbo.Articles", "Briefly");
        }
    }
}
