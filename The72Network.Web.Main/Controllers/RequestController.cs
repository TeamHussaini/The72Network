using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using The72Network.Web.Main.ViewModels;
using The72Network.Web.Services;
using The72Network.Web.Services.Request;
using The72Network.Web.Services.Shared;
using The72Network.Web.Shared.Enums;

namespace The72Network.Web.Main.Controllers
{
  public class RequestController : Controller
  {
    public RequestController()
    {
      _requestService = new RequestService();
      _sharedService = new SharedService();
    }

    public RequestController(IRequestService requestService, ISharedService sharedService)
    {
      _requestService = requestService;
      _sharedService = sharedService;
    }

    [Authorize]
    [HttpPost]
    public JsonResult SendRequest(int id)
    {
      bool success = _sharedService.TrySaveSocialType(User.Identity.Name, id, SocialTypes.Request);

      return Json(new JavaScriptSerializer().Serialize(new
      {
        success
      }));
    }

    [Authorize]
    public ViewResult GetRequests()
    {
      var pendingRequest = _requestService.GetPendingRequests(User.Identity.Name);

      var pendingUserRequests = pendingRequest.Select(x => new RequestPageViewModel
        {
          Profession = x.Profession,
          RequestId = x.RequestId,
          State = x.State,
          Tags = x.Tags,
          UserName = x.Username,
          TimeStamp = x.TimeStamp
        });
      return View("~/Views/Request/RequestsPage.cshtml", pendingUserRequests.ToList());
    }

    [Authorize]
    [HttpPost]
    public JsonResult ProcessRequest(RequestPostModel requestPageData)
    {
      RequestState state;
      bool success = Enum.TryParse<RequestState>(requestPageData.State, out state);
      success &= _requestService.ProcessRequest(state, requestPageData.RequestId);

      return Json(new JavaScriptSerializer().Serialize(new
      {
        success
      }));
    }

    [Authorize]
    public ViewResult Connections()
    {
      var connectedUsers = _requestService.GetConnectedUsers(User.Identity.Name);

      var connectedUsersModel = connectedUsers.Select(x => new CompleteUserProfileViewModel
        {
          AlmaMater = x.AlmaMater,
          City = x.City,
          Country = x.Country,
          Description = x.Description,
          DOB = x.DOB,
          EmailId = x.EmailId,
          ImageUrl = x.ImageUrl,
          MobilePhone = x.MobilePhone,
          Profession = x.Profession,
          Qualifications = x.Qualifications,
          Tags = x.Tags,
          UserName = x.UserName
        });

      return View(connectedUsersModel.ToList());
    }

    #region Privates

    private IRequestService _requestService;

    private ISharedService _sharedService;

    #endregion
  }
}