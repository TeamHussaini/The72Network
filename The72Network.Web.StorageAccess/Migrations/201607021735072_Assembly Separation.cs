namespace The72Network.Web.StorageAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssemblySeparation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Message = c.String(),
                        Correlation = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        From_Id = c.Int(),
                        To_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.From_Id)
                .ForeignKey("dbo.UserProfile", t => t.To_Id)
                .Index(t => t.From_Id)
                .Index(t => t.To_Id);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        EmailId = c.String(),
                        MobilePhone = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserExtendedProfile",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Profession = c.String(),
                        Qualifications = c.String(),
                        AlmaMater = c.String(),
                        DOB = c.String(),
                        City = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        From_Id = c.Int(),
                        To_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.From_Id)
                .ForeignKey("dbo.UserProfile", t => t.To_Id)
                .Index(t => t.From_Id)
                .Index(t => t.To_Id);
            
            CreateTable(
                "dbo.SocialManager",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SocialId = c.Int(nullable: false),
                        SocialType = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TagUserExtendedProfile",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        UserExtendedProfile_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.UserExtendedProfile_Id })
                .ForeignKey("dbo.Tag", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserExtendedProfile", t => t.UserExtendedProfile_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.UserExtendedProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SocialManager", "User_Id", "dbo.UserProfile");
            DropForeignKey("dbo.Requests", "To_Id", "dbo.UserProfile");
            DropForeignKey("dbo.Requests", "From_Id", "dbo.UserProfile");
            DropForeignKey("dbo.Messages", "To_Id", "dbo.UserProfile");
            DropForeignKey("dbo.Messages", "From_Id", "dbo.UserProfile");
            DropForeignKey("dbo.UserExtendedProfile", "Id", "dbo.UserProfile");
            DropForeignKey("dbo.TagUserExtendedProfile", "UserExtendedProfile_Id", "dbo.UserExtendedProfile");
            DropForeignKey("dbo.TagUserExtendedProfile", "Tag_Id", "dbo.Tag");
            DropIndex("dbo.TagUserExtendedProfile", new[] { "UserExtendedProfile_Id" });
            DropIndex("dbo.TagUserExtendedProfile", new[] { "Tag_Id" });
            DropIndex("dbo.SocialManager", new[] { "User_Id" });
            DropIndex("dbo.Requests", new[] { "To_Id" });
            DropIndex("dbo.Requests", new[] { "From_Id" });
            DropIndex("dbo.UserExtendedProfile", new[] { "Id" });
            DropIndex("dbo.Messages", new[] { "To_Id" });
            DropIndex("dbo.Messages", new[] { "From_Id" });
            DropTable("dbo.TagUserExtendedProfile");
            DropTable("dbo.SocialManager");
            DropTable("dbo.Requests");
            DropTable("dbo.Tag");
            DropTable("dbo.UserExtendedProfile");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Messages");
        }
    }
}
