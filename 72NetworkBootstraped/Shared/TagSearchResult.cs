using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _72NetworkBootstraped.Shared
{
  public struct TagSearchResult
  {
    public int ProfileId;

    public string UserName;

    public string ImageUrl;

    public bool MessageSentToThisUserByCurrentUser;

    public bool RequestSentToThisUserByCurrentUser;

    public TagSearchResult(int profileId, string userName, string imageUrl, bool requestSent = false, bool messageSent = false)
    {
      ProfileId = profileId;
      UserName = userName;
      ImageUrl = imageUrl;
      MessageSentToThisUserByCurrentUser = messageSent;
      RequestSentToThisUserByCurrentUser = requestSent;
    }
  }
}