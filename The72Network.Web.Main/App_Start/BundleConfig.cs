﻿using System.Web;
using System.Web.Optimization;

namespace The72Network.Web.Main
{
  public class BundleConfig
  {
    // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/plugins/jquery-1.11.2.min.js",
                  "~/Scripts/plugins/jquery-migrate-1.2.1.min.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                  "~/Scripts/jquery-ui-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.unobtrusive*",
                  "~/Scripts/jquery.validate*"));

      bundles.Add(new ScriptBundle("~/bundles/template").Include(
                  "~/Scripts/plugins/bootstrap/js/bootstrap.min.js",
                  "~/Scripts/plugins/bootstrap-hover-dropdown.min.js",
                  "~/Scripts/plugins/back-to-top.js",
                  "~/Scripts/plugins/jquery-placeholder/jquery.placeholder.js",
                  "~/Scripts/plugins/pretty-photo/js/jquery.prettyPhoto.js",
                  "~/Scripts/plugins/flexslider/jquery.flexslider-min.js",
                  "~/Scripts/plugins/jflickrfeed/jflickrfeed.min.js",
                  "~/Scripts/js/main.js"));

      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Scripts/modernizr-*"));

      bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

      bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                  "~/Content/themes/base/jquery.ui.core.css",
                  "~/Content/themes/base/jquery.ui.resizable.css",
                  "~/Content/themes/base/jquery.ui.selectable.css",
                  "~/Content/themes/base/jquery.ui.accordion.css",
                  "~/Content/themes/base/jquery.ui.autocomplete.css",
                  "~/Content/themes/base/jquery.ui.button.css",
                  "~/Content/themes/base/jquery.ui.dialog.css",
                  "~/Content/themes/base/jquery.ui.slider.css",
                  "~/Content/themes/base/jquery.ui.tabs.css",
                  "~/Content/themes/base/jquery.ui.datepicker.css",
                  "~/Content/themes/base/jquery.ui.progressbar.css",
                  "~/Content/themes/base/jquery.ui.theme.css"));
    }

  }
}