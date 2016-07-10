using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.StorageAccess.DBModels;
using The72Network.Web.StorageAccess.EntityFramework;

namespace The72Network.Web.StorageAccess.Repositories
{
  public class SocialRepository<TEntity> : Repository<TEntity>, ISocialRepository<TEntity>
    where TEntity : BaseEntity
  {
    public SocialRepository(CommonDbContext dbContext)
      : base(dbContext)
    {
    }
  }
}
