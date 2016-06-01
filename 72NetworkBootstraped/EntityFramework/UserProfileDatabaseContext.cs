using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using _72NetworkBootstraped.Models;

namespace _72NetworkBootstraped.EntityFramework
{
  public class UserProfileDatabaseContext : DbContext
  {
    public UserProfileDatabaseContext()
      : base("DefaultConnection")
    {
    }

    public DbSet<UserProfile> UserProfiles { get; set; }

    public DbSet<UserExtendedProfile> UserExtendedProfile { get; set; }

    public DbSet<Tag> Tag { get; set; }

    public DbSet<Requests> Requests { get; set; }

    public DbSet<Messages> Messages { get; set; }

    public DbSet<SocialManager> SocialManager { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }
  }
}