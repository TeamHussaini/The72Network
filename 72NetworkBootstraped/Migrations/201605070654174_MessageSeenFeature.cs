namespace _72NetworkBootstraped.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageSeenFeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "State");
        }
    }
}
