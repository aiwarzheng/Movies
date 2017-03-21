using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Optimization;
using System.IO;
using Movies.WebUI.Models;
using Movies.WebUI.Infrastructure;

namespace Movies.WebUI
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FileInfo f = new FileInfo(Server.MapPath("log4net.config"));
            log4net.Config.XmlConfigurator.Configure(f);

            ModelBinders.Binders.Add(typeof(MovieFilter), new MovieFilterBinder());
            ModelBinders.Binders.Add(typeof(Cart), new CartBinder());
        }
    }
}