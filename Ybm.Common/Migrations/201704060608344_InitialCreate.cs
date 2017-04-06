namespace Ybm.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        User_Id = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UserGroup_Id = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 64),
                        LastName = c.String(nullable: false, maxLength: 64),
                        CreationDateTime = c.DateTime(nullable: false),
                        Reputation = c.Int(nullable: false),
                        LockedCredit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActivated = c.Boolean(nullable: false),
                        MobileNumber = c.String(nullable: false, maxLength: 16),
                        ActivationCode = c.String(nullable: false, maxLength: 16),
                        UserHashKey = c.Guid(),
                        IsVerified = c.Boolean(nullable: false),
                        VerificationDateTime = c.DateTime(),
                        IsFirstLogin = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_Id, cascadeDelete: true)
                .Index(t => t.UserGroup_Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PageName = c.String(nullable: false, maxLength: 100),
                        PageHref = c.String(nullable: false, maxLength: 150),
                        IsVisible = c.Boolean(nullable: false),
                        Parent_Id = c.Int(),
                        UserGroup_Id = c.Int(nullable: false),
                        CreationDateTime = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        SelectorCssClass = c.String(nullable: false, maxLength: 50),
                        PageScope_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_Id, cascadeDelete: true)
                .Index(t => t.UserGroup_Id);
            
            CreateTable(
                "dbo.UserGroupTokens",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        UserGroup_Id = c.Int(nullable: false),
                        Token_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tokens", t => t.Token_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_Id, cascadeDelete: true)
                .Index(t => t.UserGroup_Id)
                .Index(t => t.Token_Id);
            
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        AreaName = c.String(maxLength: 16),
                        PersianName = c.String(maxLength: 128),
                        TokenCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TokenCategories", t => t.TokenCategory_Id)
                .Index(t => t.TokenCategory_Id);
            
            CreateTable(
                "dbo.TokenCategories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        PersianName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IsCustomError = c.Boolean(nullable: false),
                        Message = c.String(nullable: false),
                        InnerException = c.String(nullable: false),
                        StackTrace = c.String(nullable: false),
                        Source = c.String(nullable: false),
                        IpAddress = c.String(nullable: false, maxLength: 50),
                        CreationDateTime = c.DateTime(nullable: false),
                        ErrorType_Id = c.Byte(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ErrorTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "UserGroup_Id", "dbo.UserGroups");
            DropForeignKey("dbo.UserGroupTokens", "UserGroup_Id", "dbo.UserGroups");
            DropForeignKey("dbo.UserGroupTokens", "Token_Id", "dbo.Tokens");
            DropForeignKey("dbo.Tokens", "TokenCategory_Id", "dbo.TokenCategories");
            DropForeignKey("dbo.Pages", "UserGroup_Id", "dbo.UserGroups");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.Tokens", new[] { "TokenCategory_Id" });
            DropIndex("dbo.UserGroupTokens", new[] { "Token_Id" });
            DropIndex("dbo.UserGroupTokens", new[] { "UserGroup_Id" });
            DropIndex("dbo.Pages", new[] { "UserGroup_Id" });
            DropIndex("dbo.Users", new[] { "UserGroup_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "User_Id" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.ErrorTypes");
            DropTable("dbo.ErrorLogs");
            DropTable("dbo.TokenCategories");
            DropTable("dbo.Tokens");
            DropTable("dbo.UserGroupTokens");
            DropTable("dbo.Pages");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Users");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
        }
    }
}
