using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ecommerce01.Classes;
using Ecommerce01.Models;

namespace Ecommerce01
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //added
            //and simplify with usings
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Ecommerce01Context, Migrations.Configuration>());
            //last add
            CheckRolesAndSuperUser();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //last add
        private void CheckRolesAndSuperUser()
        {
            //developer i'am Vargas
            UsersHelper.CheckRole("Admin");
            //Altri admin of ecommerce Clients
            UsersHelper.CheckRole("User");
            //SuperUser can create Users is always Admin
            UsersHelper.CheckSuperUser();

        }

        //added
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("it-IT");
            var myCultureStr = "it-IT";
            DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(myCultureStr).DateTimeFormat;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

        }

    }
}
//string[] cultures = { "en-US", "ja-JP", "fr-FR" };
//DateTime date1 = new DateTime(2011, 5, 1);

//Console.WriteLine(" {0,7} {1,19} {2,10}\n", "CULTURE", "PROPERTY VALUE", "DATE");

//      foreach (var culture in cultures) {
//         DateTimeFormatInfo dtfi = CultureInfo.CreateSpecificCulture(culture).DateTimeFormat;
//Console.WriteLine(" {0,7} {1,19} {2,10}", culture, 
//                           dtfi.ShortDatePattern, 
//                           date1.ToString("d", dtfi));
//      }