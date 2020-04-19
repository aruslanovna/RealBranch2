namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plants",
                c => new
                    {
                        PlantId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        SpeciestId = c.Int(),
                        Photo = c.Binary(),
                        NumberOfSorts = c.Int(nullable: false),
                        Kind = c.String(),
                    })
                .PrimaryKey(t => t.PlantId)
                .ForeignKey("dbo.Species", t => t.SpeciestId)
                .Index(t => t.SpeciestId);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        SpeciestId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SpeciestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plants", "SpeciestId", "dbo.Species");
            DropIndex("dbo.Plants", new[] { "SpeciestId" });
            DropTable("dbo.Species");
            DropTable("dbo.Plants");
        }
    }
}
