using System.Web;
using System.Web.Optimization;

namespace Ecommerce01
{
    public class BundleConfig
    {
        // Per altre informazioni sulla creazione di bundle, vedere https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilizzare la versione di sviluppo di Modernizr per eseguire attività di sviluppo e formazione. Successivamente, quando si è
            // pronti per passare alla produzione, usare lo strumento di compilazione disponibile all'indirizzo https://modernizr.com per selezionare solo i test necessari.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                       "~/Scripts/efor_prov.js",
                        "~/Scripts/efor_citta.js",
                        "~/Scripts/efor_aziend.js",
                       "~/Scripts/script.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery")
            //               .Include("~/Scripts/script.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/PagedList.css",
                      "~/Content/site.css"));
        }
    }
}