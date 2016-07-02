using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using The72Network.Web.StorageAccess.DBModels;

namespace The72Newtork.Web.Shared.Extensions
{
  public static class DBExtensionMethods
  {
    public static void Update<T>(this T entity, DbContext dbContext) where T : BaseEntity
    {
      if (entity == null)
      {
        throw new ArgumentException("Cannot add a null entity.");
      }

      var entry = dbContext.Entry<T>(entity);
      if (entry.State == EntityState.Detached)
      {
        var set = dbContext.Set<T>();
        T attachedEntity = set.Local.SingleOrDefault(e => e.Id == entity.Id);  // You need to have access to key

        if (attachedEntity != null)
        {
          var attachedEntry = dbContext.Entry(attachedEntity);
          attachedEntry.CurrentValues.SetValues(entity);
        }
        else
        {
          entry.State = EntityState.Modified; // This should attach entity
        }
      }
    }
  }
}