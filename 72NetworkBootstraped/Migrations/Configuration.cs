using WebMatrix.WebData;

namespace _72NetworkBootstraped.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using _72NetworkBootstraped.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<_72NetworkBootstraped.EntityFramework.UserProfileDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_72NetworkBootstraped.EntityFramework.UserProfileDatabaseContext";
        }

        protected override void Seed(_72NetworkBootstraped.EntityFramework.UserProfileDatabaseContext context)
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
