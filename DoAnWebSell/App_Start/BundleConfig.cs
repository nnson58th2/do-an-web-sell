﻿using System.Web;
using System.Web.Optimization;

namespace DoAnWebSell
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/core").Include(
                      "~/assets/client/css/bootstrap.css",
                      "~/assets/client/css/jquery-ui.css",
                      "~/assets/client/css/fontawesome-all.css",
                      "~/assets/client/css/popuo-box.css",
                      "~/assets/client/css/menu.css",
                      "~/assets/client/css/flexslider.css",
                      "~/assets/client/css/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/controller").Include(
                    "~/assets/client/js/controller/baseController.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
