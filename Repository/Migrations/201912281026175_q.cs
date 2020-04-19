namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class q : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "HouseId", "dbo.Houses");
            DropForeignKey("dbo.Cares", "GreybeardId", "dbo.Greybeards");
            DropForeignKey("dbo.Greybeards", "Days_DayId", "dbo.Days");
            DropForeignKey("dbo.Prescriptions", "GreybeardId", "dbo.Greybeards");
            DropIndex("dbo.Cares", new[] { "GreybeardId" });
            DropIndex("dbo.Departments", new[] { "HouseId" });
            DropIndex("dbo.Greybeards", new[] { "Days_DayId" });
            DropIndex("dbo.Prescriptions", new[] { "GreybeardId" });
            DropTable("dbo.Cares");
            DropTable("dbo.Days");
            DropTable("dbo.Departments");
            DropTable("dbo.Houses");
            DropTable("dbo.Greybeards");
            DropTable("dbo.Prescriptions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        PrescriptionId = c.Int(nullable: false, identity: true),
                        GreybeardId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PrescriptionId);
            
            CreateTable(
                "dbo.Greybeards",
                c => new
                    {
                        GreybeardId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Number = c.String(),
                        Address = c.String(),
                        Photo = c.Binary(),
                        Day = c.Int(nullable: false),
                        Days_DayId = c.Int(),
                    })
                .PrimaryKey(t => t.GreybeardId);
            
            CreateTable(
                "dbo.Houses",
                c => new
                    {
                        HouseId = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 30),
                        Adres = c.String(),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HouseId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        HouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        GreybeardId = c.Int(nullable: false),
                        Name = c.String(),
                        UserId = c.String(),
                        Dinner = c.Boolean(nullable: false),
                        Breakfast = c.Boolean(nullable: false),
                        Lunch = c.Boolean(nullable: false),
                        Leisure = c.Boolean(nullable: false),
                        Walk = c.Boolean(nullable: false),
                        Description = c.String(),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.DayId);
            
            CreateTable(
                "dbo.Cares",
                c => new
                    {
                        CareId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        GreybeardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CareId);
            
            CreateIndex("dbo.Prescriptions", "GreybeardId");
            CreateIndex("dbo.Greybeards", "Days_DayId");
            CreateIndex("dbo.Departments", "HouseId");
            CreateIndex("dbo.Cares", "GreybeardId");
            AddForeignKey("dbo.Prescriptions", "GreybeardId", "dbo.Greybeards", "GreybeardId", cascadeDelete: true);
            AddForeignKey("dbo.Greybeards", "Days_DayId", "dbo.Days", "DayId");
            AddForeignKey("dbo.Cares", "GreybeardId", "dbo.Greybeards", "GreybeardId", cascadeDelete: true);
            AddForeignKey("dbo.Departments", "HouseId", "dbo.Houses", "HouseId", cascadeDelete: true);
        }
    }
}
