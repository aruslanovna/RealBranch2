namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IdentityUserClaims", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.IdentityUserLogins", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.IdentityUserRoles", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Notes", "AppUser_Id", "dbo.AppUsers");
            DropIndex("dbo.Notes", new[] { "AppUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "AppUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "AppUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "AppUser_Id" });
            DropColumn("dbo.Notes", "AppUser_Id");
            DropTable("dbo.AppUsers");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Status = c.Int(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Notes", "AppUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.IdentityUserRoles", "AppUser_Id");
            CreateIndex("dbo.IdentityUserLogins", "AppUser_Id");
            CreateIndex("dbo.IdentityUserClaims", "AppUser_Id");
            CreateIndex("dbo.Notes", "AppUser_Id");
            AddForeignKey("dbo.Notes", "AppUser_Id", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.IdentityUserRoles", "AppUser_Id", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.IdentityUserLogins", "AppUser_Id", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.IdentityUserClaims", "AppUser_Id", "dbo.AppUsers", "Id");
        }
    }
}
