using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using _72NetworkBootstraped.EntityFramework;
using _72NetworkBootstraped.Models;
using _72NetworkBootstraped.Shared;

namespace _72NetworkBootstraped.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      ViewBag.TagList = Util.TagList;
      return View();
    }

    [HttpPost]
    public JsonResult TagSearch(int[] tags)
    {
      if (tags == null)
      {
        return null;
      }
      using (UserProfileDatabaseContext dbContext = new UserProfileDatabaseContext())
      {
        Dictionary<int, Tag> tagMap = dbContext.Tag.ToDictionary(x => x.Id, x => x);
        List<Tuple<int, string>> userList = new List<Tuple<int, string>>();
        foreach (int id in tags)
        {
          Tag tag;
          if (tagMap.TryGetValue(id, out tag))
          {
            userList.AddRange(tag.Users.Select(userProfile => new Tuple<int, string>(userProfile.UserProfile.Id, userProfile.UserProfile.UserName)));
          }
        }

        return Json(new JavaScriptSerializer().Serialize(userList));
      }
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
  }
}
