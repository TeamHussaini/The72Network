using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using The72Network.Web.StorageAccess;
using The72Network.Web.StorageAccess.DBModels;

namespace The72Network.Web.Services.Account
{
  public interface IAccountService
  {
    IList<string> GetCountries();

    IList<string> GetAdmins(string adminsFromAppSetting);

    void AddUser(string username, string country, string emailId, string mobilePhone);

    bool IsUserUnique(string username);

    bool IsEmailUnique(string emailId);

    UserProfile GetUserProfile(string username);

    IList<Tag> GetTags();

    IList<string> GetProfessionsList();

    void AddUserExtendedProfile(string username, string almamater, string city, string dob, string profession, string qualifications, string description, IList<int> selectedTags);

    bool TryUploadImage(string username, HttpPostedFileBase file);

    void SendMail(string username, string emailId);
  }
}
