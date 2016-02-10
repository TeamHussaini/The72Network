using System.Web;
using System.Web.Mvc;

namespace _72NetworkBootstraped
{
  public class FilterConfig
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }
  }
}