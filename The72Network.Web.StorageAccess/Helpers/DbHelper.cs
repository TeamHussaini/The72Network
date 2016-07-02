using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using The72Network.Web.StorageAccess.DBModels;
using The72Network.Web.StorageAccess.EntityFramework;

namespace The72Network.Web.StorageAccess.Helpers
{
  public class DbHelper
  {

    private static readonly Lazy<List<Tag>> m_tags = new Lazy<List<Tag>>(() =>
    {
      using (CommonDbContext dbContext = new CommonDbContext())
      {
        return dbContext.Tag.ToList();
      }
    }, LazyThreadSafetyMode.ExecutionAndPublication);

    public static List<Tag> TagList
    {
      get
      {
        return m_tags.Value;
      }
    }
  }
}
