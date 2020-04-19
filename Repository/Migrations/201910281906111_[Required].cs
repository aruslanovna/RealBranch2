namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Articles", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Articles", "Briefly", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Topics", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Plants", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Plants", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Followers", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.Followers", "FollowerId", c => c.String(nullable: false));
            AlterColumn("dbo.Followings", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.Followings", "FollowingId", c => c.String(nullable: false));
            AlterColumn("dbo.Notes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Notes", "Photo", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "Photo", c => c.Binary());
            AlterColumn("dbo.Notes", "Name", c => c.String());
            AlterColumn("dbo.Followings", "FollowingId", c => c.String());
            AlterColumn("dbo.Followings", "UserId", c => c.String());
            AlterColumn("dbo.Followers", "FollowerId", c => c.String());
            AlterColumn("dbo.Followers", "UserId", c => c.String());
            AlterColumn("dbo.Plants", "Description", c => c.String());
            AlterColumn("dbo.Plants", "Name", c => c.String());
            AlterColumn("dbo.Topics", "Name", c => c.String());
            AlterColumn("dbo.Comments", "Content", c => c.String());
            AlterColumn("dbo.Articles", "Briefly", c => c.String());
            AlterColumn("dbo.Articles", "Description", c => c.String());
            AlterColumn("dbo.Articles", "Name", c => c.String());
        }
    }
}
