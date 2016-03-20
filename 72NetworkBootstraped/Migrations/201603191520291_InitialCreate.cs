namespace _72NetworkBootstraped.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
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
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.Id)
                .Index(t => t.Id);
            
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
                "dbo.UserExtendedProfileTag",
                c => new
                    {
                        UserExtendedProfile_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserExtendedProfile_Id, t.Tag_Id })
                .ForeignKey("dbo.UserExtendedProfile", t => t.UserExtendedProfile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.UserExtendedProfile_Id)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserExtendedProfile", "Id", "dbo.UserProfile");
            DropForeignKey("dbo.UserExtendedProfileTag", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.UserExtendedProfileTag", "UserExtendedProfile_Id", "dbo.UserExtendedProfile");
            DropIndex("dbo.UserExtendedProfileTag", new[] { "Tag_Id" });
            DropIndex("dbo.UserExtendedProfileTag", new[] { "UserExtendedProfile_Id" });
            DropIndex("dbo.UserExtendedProfile", new[] { "Id" });
            DropTable("dbo.UserExtendedProfileTag");
            DropTable("dbo.UserProfile");
            DropTable("dbo.UserExtendedProfile");
            DropTable("dbo.Tag");
        }
    }
}
