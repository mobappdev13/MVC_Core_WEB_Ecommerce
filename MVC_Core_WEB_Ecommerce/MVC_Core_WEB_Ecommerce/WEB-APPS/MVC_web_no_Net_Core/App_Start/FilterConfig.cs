using System.Web;
using System.Web.Mvc;

namespace MVC_web_no_Net_Core
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
