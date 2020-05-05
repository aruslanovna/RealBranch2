namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Photo", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Photo", c => c.Byte());
        }
    }
}
