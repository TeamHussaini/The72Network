using System;
using System.Diagnostics;
using System.Linq;
using The72Network.Web.Shared;
using The72Network.Web.Shared.Enums;
using The72Network.Web.StorageAccess.DBModels;
using The72Network.Web.StorageAccess.UnitOfWork;

namespace The72Network.Web.Services.Shared
{
  public class SharedService : ISharedService
  {
    public bool TrySaveSocialType(string username, int profileId, Web.Shared.Enums.SocialTypes socialType)
    {
      bool success = false;
      try
      {
        using (var unitOfWork = new UnitOfWork())
        {
          var user = unitOfWork.UserProfileRepository.Get(entity => entity.UserName == username).FirstOrDefault();
          var socialRecord = unitOfWork.SocialManagerRepository.Get(e => e.SocialType == socialType && e.User.Id == user.Id).FirstOrDefault();
          var currentTime = DateTime.UtcNow;
          int days = socialRecord != null ? ((TimeSpan)currentTime.Subtract((DateTime)socialRecord.TimeStamp)).Days : 0;
          int? socialId;

          if (days <= Constants.ThresholdDays && socialRecord != null)
          {
            if (socialRecord.Count < Constants.ThresholdSocialTypeCount)
            {
              success = socialType == SocialTypes.Request ?
                TrySaveRequest(profileId, user.Id, unitOfWork, out socialId) :
                TrySaveMessage(profileId, user, out socialId);

              if (!success)
              {
                return success;
              }
              socialRecord.Count++;
              unitOfWork.SocialManagerRepository.Update(socialRecord);
            }
            else
            {
              // No. of counts exceeded for the given period.
              success = false;
            }
          }
          else
          {
            success = socialType == SocialTypes.Request ?
              TrySaveRequest(profileId, user.Id, unitOfWork, out socialId) :
              TrySaveMessage(profileId, user, out socialId);

            if (!success)
            {
              return success;
            }

            if (socialRecord != null)
            {
              socialRecord.TimeStamp = DateTime.UtcNow;
              socialRecord.SocialId = socialId.GetValueOrDefault();
              socialRecord.Count = 1;
              unitOfWork.SocialManagerRepository.Update(socialRecord);
            }
            else
            {
              SocialManager newSocialRecord = new SocialManager
              {
                User = user,
                SocialId = socialId.GetValueOrDefault(),
                SocialType = socialType,
                TimeStamp = DateTime.UtcNow,
                Count = 1,
              };

              unitOfWork.SocialManagerRepository.Insert(newSocialRecord);
            }
          }

          unitOfWork.Save();
        }
      }
      catch (Exception ex)
      {
        Trace.TraceError("Operation failed with following ex : {0}", ex.ToString());

        return false;
      }

      return success;
    }

    #region Privates

    private bool TrySaveRequest(int profileId, int fromUserId, UnitOfWork unitOfWork, out int? socialId)
    {
      socialId = -1;
      try
      {
        var toUser = unitOfWork.UserProfileRepository.Get(e => e.Id == profileId).FirstOrDefault();
        var fromUser = unitOfWork.UserProfileRepository.Get(e => e.Id == fromUserId).FirstOrDefault();
        bool dublicateRequest = unitOfWork.RequestRepository.Get(r => r.To.Id == toUser.Id && r.From.Id == fromUser.Id).Any();
        if (dublicateRequest)
        {
          return false;
        }
        Requests request = new Requests
        {
          To = toUser,
          From = fromUser,
          Time = DateTime.UtcNow,
          State = RequestState.Pending
        };

        unitOfWork.RequestRepository.Insert(request);
        socialId = request.Id;
      }
      catch (Exception ex)
      {
        Trace.TraceError("Faild with following ex : {0}", ex.ToString());

        return false;
      }

      return true;
    }

    private bool TrySaveMessage(int profileId, UserProfile fromUser, out int? socialId)
    {
      socialId = -1;

      return true;
    }

    #endregion
  }
}
