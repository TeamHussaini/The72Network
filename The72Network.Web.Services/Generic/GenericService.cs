using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using The72Network.Web.Shared.Enums;
using The72Network.Web.StorageAccess.UnitOfWork;
using The72Network.Web.StorageAccess.DBModels;
using The72Newtork.Web.Shared.Wrappers;

namespace The72Network.Web.Services.Generic
{
  public class GenericService : IGenericService
  {
    public string GetUserFromTagsAsString(string username, IEnumerable<int> tagIds, TagSearchConfiguration tagSearchConfig, int tagsCount)
    {
      var userList = new List<TagSearchResult>();
      var uniqueUsers = new Dictionary<int, int>();
      using (var unitOfWork = new UnitOfWork())
      {
        var tagMap = unitOfWork.TagRepository.Get(_ => true).ToDictionary(x => x.Id, x => x);
        var userProfile = unitOfWork.UserProfileRepository.Get(entity => entity.UserName == username).FirstOrDefault();
        var requestedUsers = unitOfWork.RequestRepository.Get(entity => entity.From.Id == userProfile.Id).Select(x => x.To.Id).ToList();
        foreach (var id in tagIds)
        {
          Tag tag;
          if (tagMap.TryGetValue(id, out tag))
          {
            foreach (UserExtendedProfile user in tag.Users)
            {
              if (!uniqueUsers.ContainsKey(user.UserProfile.Id) && userProfile.Id != user.UserProfile.Id)
              {
                userList.Add(new TagSearchResult(user.UserProfile.Id, user.UserProfile.UserName,
                  user.ImageUrl, requestSent: requestedUsers.Contains(user.UserProfile.Id)));
                uniqueUsers[user.UserProfile.Id] = 1;
              }
              else
              {
                if (uniqueUsers.ContainsKey(user.UserProfile.Id))
                {
                  uniqueUsers[user.UserProfile.Id]++;
                }
              }
            }
          }
        }
      }
      if (tagSearchConfig == TagSearchConfiguration.Intersection)
      {
        // Intersection
        return new JavaScriptSerializer().Serialize(userList.Where(x => uniqueUsers[x.ProfileId] == tagsCount));
      }

      // Union
      return new JavaScriptSerializer().Serialize(userList);
    }
  }
}
