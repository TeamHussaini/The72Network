using System.Web;
using System.Web.Mvc;

namespace The72Network.Web.Main
{
  public class FilterConfig
  {
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }
  }
}