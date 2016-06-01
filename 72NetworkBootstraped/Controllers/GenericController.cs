using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using _72NetworkBootstraped.EntityFramework;
using _72NetworkBootstraped.Models;
using _72NetworkBootstraped.Shared;

namespace _72NetworkBootstraped.Controllers
{
  public class GenericController : Controller
  {
    #region Controllers

    [Authorize]
    [HttpPost]
    public JsonResult SendRequest(int id)
    {
      bool success = TrySaveSocialType(id, SocialTypes.Request);

      return Json(new JavaScriptSerializer().Serialize(new { success }));
    }

    [Authorize]
    public ViewResult GetRequests()
    {
      using (UserProfileDatabaseContext dbContext = new UserProfileDatabaseContext())
      {
        UserProfile currentUser = dbContext.UserProfiles.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
        IEnumerable<RequestPageViewModel> pendingUserRequests = dbContext.Requests.Where(r => r.To.Id == currentUser.Id && r.State == RequestState.Pending).Select(x => new RequestPageViewModel
        {
          Profession = x.From.UserExtendedProfile.Profession,
          UserName = x.From.UserName,
          RequestId = x.Id,
          State = x.State,
          TimeStamp = x.Time,
          Tags = x.From.UserExtendedProfile.Tags.Select(t => t.TagName).ToList()
        });

        return View("~/Views/Generic/RequestsPage.cshtml", pendingUserRequests.ToList());
      }
    }

    [Authorize]
    [HttpPost]
    public JsonResult ProcessRequest(RequestPostModel requestPageData)
    {
      bool success = true;
      try
      {
        RequestState state = (RequestState)Enum.Parse(typeof(RequestState), requestPageData.State, true);
        if (state == RequestState.Pending)
        {
          return Json(new JavaScriptSerializer().Serialize(new { success }));
        }
        using (UserProfileDatabaseContext dbContext = new UserProfileDatabaseContext())
        {
          Requests request = dbContext.Requests.Where(r => r.Id == requestPageData.RequestId).FirstOrDefault();
          Requests newRequest = new Requests
          {
            Id = request.Id,
            From = request.From,
            To = request.To,
            Time = request.Time,
            State = (RequestState)Enum.Parse(typeof(RequestState), requestPageData.State, true)
          };

          newRequest.Update(dbContext);
          dbContext.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.Write("Failed with following ex : {0}", ex.ToString());
        success = false;
      }

      return Json(new JavaScriptSerializer().Serialize(new { success }));
    }

    [Authorize]
    public ViewResult Connections()
    {
      using (UserProfileDatabaseContext dbContext = new UserProfileDatabaseContext())
      {
        UserProfile currentUser = dbContext.UserProfiles.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
        IEnumerable<CompleteUserProfileViewModel> connectedUsers = dbContext.Requests.Where(r => (r.To.Id == currentUser.Id || r.From.Id == currentUser.Id) && r.State == RequestState.Accepted).Select(x => new CompleteUserProfileViewModel
        {
          AlmaMater = x.From.UserExtendedProfile.AlmaMater,
          City = x.From.UserExtendedProfile.City,
          Country = x.From.Country,
          Description = x.From.UserExtendedProfile.Description,
          DOB = x.From.UserExtendedProfile.DOB,
          EmailId = x.From.EmailId,
          ImageUrl = x.From.UserExtendedProfile.ImageUrl,
          MobilePhone = x.From.MobilePhone,
          Profession = x.From.UserExtendedProfile.Profession,
          Qualifications = x.From.UserExtendedProfile.Qualifications,
          Tags = x.From.UserExtendedProfile.Tags.Select(t => t.TagName).ToList(),
          UserName = x.From.UserName
        });

        return View(connectedUsers.ToList());
      }
    }

    [Authorize]
    public JsonResult SendMessage()
    {
      return Json(new JavaScriptSerializer().Serialize(new { success = true }));
    }

    public PartialViewResult NavbarData()
    {
      if (!Request.IsAuthenticated)
      {
        return PartialView("~/Views/Shared/_NavbarPartial.cshtml");
      }
      using (UserProfileDatabaseContext dbContext = new UserProfileDatabaseContext())
      {
        UserProfile currentUser = dbContext.UserProfiles.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
        int requestCount = dbContext.Requests.Where(r => r.To.Id == currentUser.Id && r.State == RequestState.Pending).Count();
        int messageCount = dbContext.Messages.Where(r => r.To.Id == currentUser.Id).Count();

        NavbarViewModel navbarModel = new NavbarViewModel
        {
          NumberOfMessages = messageCount,
          NumberOfRequests = requestCount
        };
        return PartialView("~/Views/Shared/_NavbarPartial.cshtml", navbarModel);
      }
    }

    #endregion

    #region Helper Methods

    private bool TrySaveSocialType(int profileId, SocialTypes socialType)
    {
      bool success = false;
      try
      {
        using (UserProfileDatabaseContext dbContext = new UserProfileDatabaseContext())
        {
          // Get the SocialManager handle first
          UserProfile currentUser = dbContext.UserProfiles.FirstOrDefault(u => u.UserName == User.Identity.Name);
          SocialManager socialRecord = dbContext.SocialManager.Where(x => x.SocialType == socialType && x.User.Id == currentUser.Id).FirstOrDefault();
          DateTime currentTime = DateTime.UtcNow;
          int days = socialRecord != null ? ((TimeSpan)currentTime.Subtract((DateTime)socialRecord.TimeStamp)).Days : 0;
          int? socialId;

          if (days <= ThresholdDays && socialRecord != null)
          {
            if (socialRecord.Count < ThresholdSocialTypeCount)
            {
              success = socialType == SocialTypes.Request ? TrySaveRequest(profileId, currentUser, dbContext, out socialId) : TrySaveMessage(profileId, currentUser, out socialId);
              if (!success)
              {
                return success;
              }
              socialRecord.Count++;
              socialRecord.Update(dbContext);
            }
            else
            {
              // No. of counts exceeded for the given period.
              success = false;
            }
          }
          else
          {
            success = socialType == SocialTypes.Request ? TrySaveRequest(profileId, currentUser, dbContext, out socialId) : TrySaveMessage(profileId, currentUser, out socialId);
            if (!success)
            {
              return success;
            }

            if (socialRecord != null)
            {
              socialRecord.TimeStamp = DateTime.UtcNow;
              socialRecord.SocialId = socialId.GetValueOrDefault();
              socialRecord.Count = 1;
              socialRecord.Update(dbContext);
            }
            else
            {
              SocialManager newSocialRecord = new SocialManager
              {
                User = currentUser,
                SocialId = socialId.GetValueOrDefault(),
                SocialType = socialType,
                TimeStamp = DateTime.UtcNow,
                Count = 1,
              };

              dbContext.SocialManager.Add(newSocialRecord);
            }
          }

          dbContext.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.Write("Operation failed with following ex : {0}", ex.ToString());

        return false;
      }

      return success;
    }

    private bool TrySaveRequest(int profileId, UserProfile fromUser, UserProfileDatabaseContext dbContext, out int? socialId)
    {
      socialId = -1;
      try
      {
        UserProfile toUser = dbContext.UserProfiles.FirstOrDefault(p => p.Id == profileId);
        bool dublicateRequest = dbContext.Requests.Where(r => r.To.Id == toUser.Id && r.From.Id == fromUser.Id).Any();
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

        dbContext.Requests.Add(request);
        dbContext.SaveChanges();
        socialId = request.Id;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.Write("Faild with following ex : {0}", ex.ToString());

        return false;
      }

      return true;
    }

    private bool TrySaveMessage(int profileId, UserProfile fromUser, out int? socialId)
    {
      socialId = -1;
      using (UserProfileDatabaseContext dbContext = new UserProfileDatabaseContext())
      {
        UserProfile toUser = dbContext.UserProfiles.FirstOrDefault(p => p.Id == profileId);

      }

      return true;
    }

    #endregion

    #region Constants

    private const int ThresholdDays = 7;

    private const int ThresholdSocialTypeCount = 20;

    #endregion
  }
}