using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.StorageAccess.EntityFramework;

namespace The72Network.Web.StorageAccess.Repositories
{
  public interface IRepository<TEntity>
  {
    TEntity GetById(int id);

    void Insert(TEntity entity);

    void Delete(int id);

    void Delete(TEntity entity);

    void Update(TEntity entity);

    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
      string includeProperties = "");
  }
}
