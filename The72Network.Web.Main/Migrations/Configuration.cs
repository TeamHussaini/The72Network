using System.Collections.Generic;
using System.Data.Entity.Migrations;
using The72Network.Web.StorageAccess.DBModels;
using The72Network.Web.StorageAccess.EntityFramework;
using WebMatrix.WebData;

namespace The72Network.Web.Main.Migrations
{
  internal sealed class Configuration : DbMigrationsConfiguration<CommonDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
      ContextKey = "The72Network.Web.StorageAccess.EntityFramework.UserProfileDatabaseContext";
    }

    protected override void Seed(CommonDbContext context)
    {
      var tags = new List<Tag>
          {
            new Tag {TagName = "ComputerScience"},
            new Tag {TagName = "Mining"},
            new Tag {TagName = "Photography"},
            new Tag {TagName = "Blogging"}
          };

      tags.ForEach(t => context.Tag.AddOrUpdate(x => x.TagName, t));
      context.SaveChanges();

      SeedMemberShip();
    }

    private static void SeedMemberShip()
    {
      WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "Id", "UserName", autoCreateTables: true);
    }
  }
}
