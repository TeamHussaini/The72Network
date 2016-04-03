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
      using (UserProfileDatabaseContext dbContext = new UserProfileDatabaseContext())
      {
        Dictionary<int, Tag> tagMap = dbContext.Tag.ToDictionary(x => x.Id, x => x);
        List<Tuple<int, string, string>> userList = new List<Tuple<int, string, string>>();
        Dictionary<int, int> uniqueUsers = new Dictionary<int, int>();
        foreach (int id in tags)
        {
          Tag tag;
          if (tagMap.TryGetValue(id, out tag))
          {
            foreach (UserExtendedProfile user in tag.Users)
            {
              if (!uniqueUsers.ContainsKey(user.UserProfile.Id))
              {
                userList.Add(Tuple.Create<int, string, string>(user.UserProfile.Id, user.UserProfile.UserName,
                  user.ImageUrl));
                uniqueUsers[user.UserProfile.Id] = 1;
              }
              else
              {
                uniqueUsers[user.UserProfile.Id]++;
              }
            }
          }
        }

        // Configuration 0 : Union ; Configuration 1 : Intersection
        if (configuration == 1)
        {
          // Intersecting
          return Json(new JavaScriptSerializer().Serialize(userList.Where(x => uniqueUsers[x.Item1] == tags.Length)));
        }

        // Union
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
