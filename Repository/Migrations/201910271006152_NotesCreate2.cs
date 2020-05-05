namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotesCreate2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Check = c.Boolean(nullable: false),
                        UserId = c.String(),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.NoteId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notes");
        }
    }
}
