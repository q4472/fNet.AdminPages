using System.Web.Mvc;
using System.Web.Routing;

namespace FNet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "adminpages/f0/save",
                defaults: new { controller = "F0", action = "Save" });

            routes.MapRoute(
                name: null,
                url: "adminpages/f0/applyfilter",
                defaults: new { controller = "F0", action = "ApplyFilter" });

            routes.MapRoute(
                name: null,
                url: "adminpages/f0",
                defaults: new { controller = "F0", action = "Index" });

            routes.MapRoute(
                name: null,
                url: "{*pathInfo}",
                defaults: new { controller = "Home", action = "Index" });
        }
    }
}
