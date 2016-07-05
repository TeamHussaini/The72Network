using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using The72Network.Web.Services.Generic;
using The72Network.Web.Shared.Enums;
using The72Network.Web.StorageAccess.DBModels;
using The72Network.Web.StorageAccess.EntityFramework;
using The72Network.Web.StorageAccess.Helpers;
using The72Newtork.Web.Shared.Wrappers;

namespace The72Network.Web.Main.Controllers
{
  public class HomeController : Controller
  {
    public HomeController()
    {
      _genericService = new GenericService();
    }

    public HomeController(IGenericService genericService)
    {
      _genericService = genericService;
    }

    public ActionResult Index()
    {
      Session["ActiveNavbar"] = "Home";
      ViewBag.TagList = DbHelper.TagList;
      return View();
    }

    [Authorize]
    [HttpPost]
    public JsonResult TagSearch(int[] data)
    {
      // Last element of data contains configuration instead of tags, tread carefully!
      int configuration = data[data.Length - 1];
      int[] tags = new int[data.Length-1];
      Array.Copy(data, tags, data.Length-1);
      if (tags.Length <= 0)
      {
        return null;
      }
 
      TagSearchConfiguration tagSearchConfig = (TagSearchConfiguration)configuration;
      return Json(_genericService.GetUserFromTagsAsString(User.Identity.Name, tags, tagSearchConfig, tags.Length));
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your app description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }

    #region Privates

    private IGenericService _genericService;

    #endregion
  }
}
