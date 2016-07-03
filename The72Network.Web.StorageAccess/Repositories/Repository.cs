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
  public class Repository<TEntity> : IRepository<TEntity>, IDisposable
    where TEntity : class
  {
    public Repository(CommonDbContext dbContext)
    {
      _dbContext = dbContext;
      _dbSet = _dbContext.Set<TEntity>();
    }

    #region Operations

    public TEntity GetById(int id)
    {
      return _dbSet.Find(id);
    }

    public void Insert(TEntity entity)
    {
      _dbSet.Add(entity);
    }

    public void Delete(int id)
    {
      TEntity entity = _dbSet.Find(id);
      Delete(entity);
    }

    public void Delete(TEntity entity)
    {
      if (_dbContext.Entry(entity).State == EntityState.Detached)
      {
        _dbSet.Attach(entity);
      }

      _dbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
      _dbSet.Attach(entity);
      _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
       string includeProperties = "")
    {
      IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    #endregion

    public void Save()
    {
      _dbContext.SaveChanges();
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isDisposing)
    {
      if (!_isDisposed)
      {
        if (isDisposing)
        {
          _dbContext.Dispose();
        }
      }

      _isDisposed = true;
    }

    #region Privates

    private CommonDbContext _dbContext;

    private DbSet<TEntity> _dbSet;

    private bool _isDisposed = false;

    #endregion
  }
}
