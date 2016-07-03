using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.StorageAccess.DBModels;
using The72Network.Web.StorageAccess.EntityFramework;
using The72Network.Web.StorageAccess.Helpers;
using The72Network.Web.StorageAccess.Repositories;
using The72Newtork.Web.Shared.Utilities;

namespace The72Network.Web.Services.Account
{
  public class AccountService : IAccountService
  {
    public IList<string >GetCountries()
    {
      return Util.ListOfCountries();
    }

    public void AddUser(string username, string country, string emailId, string mobilePhone)
    {
      UserProfile user = new UserProfile()
      {
        UserName = username,
        Country = country,
        EmailId = emailId,
        MobilePhone = mobilePhone
      };

      using(var userProfileRepository = new Repository<UserProfile>(new CommonDbContext()))
      {
        userProfileRepository.Insert(user);
        userProfileRepository.Save();
      }
    }

    public bool IsUserUnique(string username)
    {
      bool userExists;
      using(var userProfileRepository = new Repository<UserProfile>(new CommonDbContext()))
      {
        userExists = userProfileRepository.Get(entity => entity.UserName == username).Any();
      }

      return !userExists;
    }

    public bool IsEmailUnique(string emailId)
    {
      bool emailIdExists;
      using(var userProfileRepository = new Repository<UserProfile>(new CommonDbContext()))
      {
        emailIdExists = userProfileRepository.Get(entity => entity.EmailId == emailId).Any();
      }

      return !emailIdExists;
    }

    public UserProfile GetUserProfile(string username)
    {
      UserProfile user;
      using(var userProfileRepository = new Repository<UserProfile>(new CommonDbContext()))
      {
        user = userProfileRepository.Get(entity => entity.UserName == username).FirstOrDefault();
      }

      return user;
    }

    public IList<Tag> GetTags()
    {
      return DbHelper.TagList;
    }

    public IList<string> GetProfessionsList()
    {
      return Util.ListOfProfessions;
    }
  }
}
