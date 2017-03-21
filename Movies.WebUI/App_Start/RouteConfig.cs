using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Movies.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.RouteExistingFiles = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*assets}", new { assets = @"(.*/)*(.*\.(css|js|gif|jpg|jpeg|png))" });  //ignore all resource files

            routes.MapRoute(
                "api",
                "api/{action}/{id}",
                new { controller = "Api", action = "Movies", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "cart",
                "cart/{action}/{id}",
                new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{action}/{id}",
                defaults: new { controller = "Movies", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
