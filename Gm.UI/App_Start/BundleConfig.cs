﻿using System.Web.Optimization;

namespace Gm.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                        "~/Scripts/select2.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/gestion/bootstrap").Include(
                   "~/Scripts/gestion/bootstrap.min.js",
                   "~/Scripts/gestion/jquery.validate.min.js",
                    "~/Scripts/gestion/messages_fr.min.js",
                   "~/Scripts/gestion/site.js",
                   "~/Scripts/jquery.confirm.min.js",
                    "~/Scripts/jquery.unobtrusive-ajax.min.js",
                     "~/Scripts/select2.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/select2-bootstrap.css",
                       "~/Content/select2.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/Gestion/css").Include(
                   "~/Content/Gestion/css/bootstrap-responsive.min.css",
                       "~/Content/Gestion/css/bootstrap.min.css",
                       "~/Content/select2-bootstrap.css",
                       "~/Content/Gestion/css/screen.css",
                       "~/Content/select2.css",
                      "~/Content/Gestion/css/site.css"
                     ));
            
           
        }
    }
}
