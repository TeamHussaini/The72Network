using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data.Entity;
using The72Network.Web.Shared;
using The72Newtork.Web.Shared.Extensions;
using The72Network.Web.Shared.Enums;
using The72Network.Web.StorageAccess.DBModels;
using The72Network.Web.StorageAccess.EntityFramework;
using The72Network.Web.Main.ViewModels;

namespace The72Network.Web.Main.Controllers
{
  public class GenericController : Controller
  {
    #region Controllers

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
      using (CommonDbContext dbContext = new CommonDbContext())
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
  }
}