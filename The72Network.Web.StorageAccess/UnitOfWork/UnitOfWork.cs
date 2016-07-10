using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.StorageAccess.DBModels;
using The72Network.Web.StorageAccess.EntityFramework;
using The72Network.Web.StorageAccess.Repositories;

namespace The72Network.Web.StorageAccess.UnitOfWork
{
  public class UnitOfWork : IDisposable
  {
    public IRepository<UserProfile> UserProfileRepository
    {
      get
      {
        if (_userProfileRepository == null)
        {
          _userProfileRepository = new Repository<UserProfile>(_dbContext);
        }

        return _userProfileRepository;
      }
    }

    public IRepository<UserExtendedProfile> UserExtendedProfileRepository
    {
      get
      {
        if (_userExtendedProfileRepository == null)
        {
          _userExtendedProfileRepository = new Repository<UserExtendedProfile>(_dbContext);
        }

        return _userExtendedProfileRepository;
      }
    }

    public IRepository<Tag> TagRepository
    {
      get
      {
        if (_tagRepository == null)
        {
          _tagRepository = new Repository<Tag>(_dbContext);
        }

        return _tagRepository;
      }
    }

    public IRepository<Requests> RequestRepository
    {
      get
      {
        if (_requestsRepository == null)
        {
          _requestsRepository = new Repository<Requests>(_dbContext);
        }

        return _requestsRepository;
      }
    }

    public IRepository<SocialManager> SocialManagerRepository
    {
      get
      {
        if (_socialManagerRepository == null)
        {
          _socialManagerRepository = new Repository<SocialManager>(_dbContext);
        }

        return _socialManagerRepository;
      }
    }

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

    private CommonDbContext _dbContext = new CommonDbContext();

    private IRepository<UserProfile> _userProfileRepository;

    private IRepository<UserExtendedProfile> _userExtendedProfileRepository;

    private IRepository<Tag> _tagRepository;

    private IRepository<Requests> _requestsRepository;

    private IRepository<SocialManager> _socialManagerRepository;

    private bool _isDisposed = false;

    #endregion
  }
}
