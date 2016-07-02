using WebMatrix.WebData;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using The72Network.Web.StorageAccess.EntityFramework;
using The72Network.Web.StorageAccess.DBModels;

namespace The72Network.Web.StorageAccess.Migrations
{

  internal sealed class Configuration : DbMigrationsConfiguration<CommonDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
      ContextKey = "_72NetworkBootstraped.EntityFramework.UserProfileDatabaseContext";
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
