namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class D : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "CommentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "CommentId", c => c.Int());
        }
    }
}
