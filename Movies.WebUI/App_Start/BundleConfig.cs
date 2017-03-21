using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Movies.WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-1.10.2.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-sanitize.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/font-awesome.min.css",
                "~/Content/bootstrap.min.css",
                "~/Content/Site.css"));
        }
    }
}