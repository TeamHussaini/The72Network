namespace _72NetworkBootstraped.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserExtendedProfile", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserExtendedProfile", "Description");
        }
    }
}
