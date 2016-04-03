using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _72NetworkBootstraped.Controllers
{
    public class GenericController : Controller
    {
        [Authorize]
        public JsonResult SendRequest(int id)
        {
          return Json("Success");
        }

        [Authorize]
        public JsonResult SendMessage()
        {
          return Json("Success");
        }
    }
}