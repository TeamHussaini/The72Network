using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.StorageAccess.DBModels;

namespace The72Network.Web.StorageAccess.EntityFramework
{
  public class CommonDbContext : DbContext
  {
    public CommonDbContext()
      : base("DefaultConnection")
    {
    }

    public DbSet<UserProfile> UserProfiles
    {
      get;
      set;
    }

    public DbSet<UserExtendedProfile> UserExtendedProfile
    {
      get;
      set;
    }

    public DbSet<Tag> Tag
    {
      get;
      set;
    }

    public DbSet<Requests> Requests
    {
      get;
      set;
    }

    public DbSet<Messages> Messages
    {
      get;
      set;
    }

    public DbSet<SocialManager> SocialManager
    {
      get;
      set;
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    }
  }
}
