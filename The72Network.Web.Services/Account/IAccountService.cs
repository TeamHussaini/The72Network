using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The72Network.Web.StorageAccess.DBModels;

namespace The72Network.Web.Services.Account
{
  public interface IAccountService
  {
    IList<string> GetCountries();

    void AddUser(string username, string country, string emailId, string mobilePhone);

    bool IsUserUnique(string username);

    bool IsEmailUnique(string emailId);

    UserProfile GetUserProfile(string username);

    IList<Tag> GetTags();

    IList<string> GetProfessionsList();
  }
}
