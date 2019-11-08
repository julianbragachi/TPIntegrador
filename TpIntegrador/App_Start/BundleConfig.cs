using System.Web;
using System.Web.Optimization;

namespace TpIntegrador
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/ui-libs").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/materialize.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Views").Include(
                        "~/Scripts/Views/_Layout.js",
                        "~/Scripts/Views/Home/Index.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/materialize.min.css",
                      "~/Content/WCManoNegraBta.otf",
                      "~/Content/Site.css"));

        }
    }
}
