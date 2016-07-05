using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using The72Network.Web.StorageAccess;
using The72Network.Web.StorageAccess.DBModels;
using The72Network.Web.StorageAccess.EntityFramework;
using The72Network.Web.StorageAccess.Helpers;
using The72Network.Web.StorageAccess.Repositories;
using The72Newtork.Web.Shared.Utilities;

namespace The72Network.Web.Services.Account
{
  public class AccountService : IAccountService
  {
    public IList<string> GetCountries()
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

      using (var userProfileRepository = new Repository<UserProfile>(new CommonDbContext()))
      {
        userProfileRepository.Insert(user);
        userProfileRepository.Save();
      }
    }

    public bool IsUserUnique(string username)
    {
      bool userExists;
      using (var userProfileRepository = new Repository<UserProfile>(new CommonDbContext()))
      {
        userExists = userProfileRepository.Get(entity => entity.UserName == username).Any();
      }

      return !userExists;
    }

    public bool IsEmailUnique(string emailId)
    {
      bool emailIdExists;
      using (var userProfileRepository = new Repository<UserProfile>(new CommonDbContext()))
      {
        emailIdExists = userProfileRepository.Get(entity => entity.EmailId == emailId).Any();
      }

      return !emailIdExists;
    }

    public UserProfile GetUserProfile(string username)
    {
      UserProfile user;
      using (var userProfileRepository = new Repository<UserProfile>(new CommonDbContext()))
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

    void IAccountService.AddUserExtendedProfile(string username, string almamater, string city, string dob, string profession, string qualifications, string description, IList<int> selectedTags)
    {
      using (var unitOfWork = new UnitOfWork())
      {
        var userRepository = unitOfWork.UserProfileRepository;
        var user = userRepository.Get(entity => entity.UserName == username).FirstOrDefault();
        var userExtendedProfile = new UserExtendedProfile
        {
          Id = user.Id,
          UserProfile = user,
          AlmaMater = almamater,
          City = city,
          DOB = dob,
          Profession = profession,
          Qualifications = qualifications,
          Description = description,
        };

        var tagMap = unitOfWork.TagRepository.Get(_ => true).ToDictionary(x => x.Id, x => x);
        foreach (int tagId in selectedTags)
        {
          Tag selectedTag = tagMap[tagId];
          userExtendedProfile.Tags.Add(selectedTag);
          selectedTag.Users.Add(userExtendedProfile);
        }

        if (user.UserExtendedProfile != null)
        {
          userExtendedProfile.ImageUrl = user.UserExtendedProfile.ImageUrl;
          unitOfWork.UserExtendedProfileRepository.Update(userExtendedProfile);
        }
        else
        {
          unitOfWork.UserExtendedProfileRepository.Insert(userExtendedProfile);
        }
        user.UserExtendedProfile = userExtendedProfile;

        userRepository.Update(user);

        unitOfWork.Save();
      }
    }

    public bool TryUploadImage(string username, HttpPostedFileBase file)
    {

      string imageName = Path.GetFileName(file.FileName);
      if (imageName == null)
      {
        return false;
      }

      //string imageExtension = imageName.Substring(imageName.IndexOf('.'));
      //imageName = imageName.Substring(0, imageName.IndexOf('.')) + "_" + User.Identity.Name + imageExtension;

      //string physicalPath = Server.MapPath("~/Images/ProfilePic");
      //physicalPath = Path.Combine(physicalPath, imageName);

      //file.SaveAs(physicalPath);

      try
      {
        UserProfile user;
        using (var unitOfWork = new UnitOfWork())
        {
          user = unitOfWork.UserProfileRepository.Get(entity => entity.UserName == username).FirstOrDefault();

          var userExtendedProfile = user.UserExtendedProfile;
          if (userExtendedProfile == null)
          {
            userExtendedProfile = new UserExtendedProfile()
            {
              Id = user.Id,
              UserProfile = user,
              ImageUrl = imageName,
            };

            unitOfWork.UserExtendedProfileRepository.Insert(userExtendedProfile);
          }
          else
          {
            userExtendedProfile.ImageUrl = imageName;
            unitOfWork.UserExtendedProfileRepository.Update(userExtendedProfile);
          }
        }
      }
      catch(Exception ex)
      {
        throw ex;
      }

      return true;
    }

  }
}
