namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotesCreate : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Notes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Check = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NoteId);
            
        }
    }
}
