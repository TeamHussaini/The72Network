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
  public class SocialUnitOfWork : IDisposable
  {
    public ISocialRepository<Requests> RequestRepository
    {
      get
      {
        if (_requestsRepository == null)
        {
          _requestsRepository = new SocialRepository<Requests>(_dbContext);
        }

        return _requestsRepository;
      }
    }

    public ISocialRepository<SocialManager> SocialManager
    {
      get
      {
        if (_socialManagerRepository == null)
        {
          _socialManagerRepository = new SocialRepository<SocialManager>(_dbContext);
        }

        return _socialManagerRepository;
      }
    }

    public ISocialRepository<Messages> MessageRepository
    {
      get
      {
        if (_messagesRepository == null)
        {
          _messagesRepository = new SocialRepository<Messages>(_dbContext);
        }

        return _messagesRepository;
      }
    }

    public ISocialRepository<UserProfile> UserProfileRepository
    {
      get
      {
        if (_userProfileRepository == null)
        {
          _userProfileRepository = new SocialRepository<UserProfile>(_dbContext);
        }

        return _userProfileRepository;
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

    private ISocialRepository<UserProfile> _userProfileRepository;

    private ISocialRepository<Requests> _requestsRepository;

    private ISocialRepository<SocialManager> _socialManagerRepository;

    private ISocialRepository<Messages> _messagesRepository;

    private bool _isDisposed = false;

    #endregion
  }
}
