namespace _72NetworkBootstraped.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendingDB : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserExtendedProfileTag", newName: "TagUserExtendedProfile");
            DropPrimaryKey("dbo.TagUserExtendedProfile");
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Message = c.String(),
                        Correlation = c.Guid(nullable: false),
                        From_Id = c.Int(),
                        To_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.From_Id)
                .ForeignKey("dbo.UserProfile", t => t.To_Id)
                .Index(t => t.From_Id)
                .Index(t => t.To_Id);
            
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
            
            AddPrimaryKey("dbo.TagUserExtendedProfile", new[] { "Tag_Id", "UserExtendedProfile_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "To_Id", "dbo.UserProfile");
            DropForeignKey("dbo.Requests", "From_Id", "dbo.UserProfile");
            DropForeignKey("dbo.Messages", "To_Id", "dbo.UserProfile");
            DropForeignKey("dbo.Messages", "From_Id", "dbo.UserProfile");
            DropIndex("dbo.Requests", new[] { "To_Id" });
            DropIndex("dbo.Requests", new[] { "From_Id" });
            DropIndex("dbo.Messages", new[] { "To_Id" });
            DropIndex("dbo.Messages", new[] { "From_Id" });
            DropPrimaryKey("dbo.TagUserExtendedProfile");
            DropTable("dbo.Requests");
            DropTable("dbo.Messages");
            AddPrimaryKey("dbo.TagUserExtendedProfile", new[] { "UserExtendedProfile_Id", "Tag_Id" });
            RenameTable(name: "dbo.TagUserExtendedProfile", newName: "UserExtendedProfileTag");
        }
    }
}
