using System.Web.Mvc;
using System.Web.Routing;

namespace BookReadingEvents
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "customer-support",
                url: "customer-support",
                defaults: new { controller = "User", action = "CustomerSupport"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "BookReadingEvents", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
