using System.Web.Mvc;
using System.Web.Routing;

namespace DeliverDancingGoatMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Empty page",
                url: "empty/{urlslug}",
                defaults: new { controller = "LandingPage", action = "Empty" }
            );

            routes.MapRoute(
                name: "Landing page",
                url: "{urlslug}",
                defaults: new { controller = "LandingPage", action = "View" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
