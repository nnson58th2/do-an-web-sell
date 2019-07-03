using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAnWebSell
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //User section
            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{metatitle}-{id}",
                defaults: new { controller = "product", action = "category", id = UrlParameter.Optional },
                namespaces: new[] { "DoAnWebSell.Controllers" }
            );

            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{metatitle}-{id}",
                defaults: new { controller = "product", action = "detail", id = UrlParameter.Optional },
                namespaces: new[] { "DoAnWebSell.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "gioi-thieu",
                defaults: new { controller = "about", action = "index", id = UrlParameter.Optional },
                namespaces: new[] { "DoAnWebSell.Controllers" }
            );

            routes.MapRoute(
               name: "All Product",
               url: "san-pham",
               defaults: new { controller = "product", action = "productAll", id = UrlParameter.Optional },
               namespaces: new[] { "DoAnWebSell.Controllers" }
           );

            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "contact", action = "index", id = UrlParameter.Optional },
               namespaces: new[] { "DoAnWebSell.Controllers" }
           );

            routes.MapRoute(
               name: "Cart",
               url: "gio-hang",
               defaults: new { controller = "cart", action = "index", id = UrlParameter.Optional },
               namespaces: new[] { "DoAnWebSell.Controllers" }
           );

            routes.MapRoute(
               name: "Add Cart",
               url: "them-gio-hang",
               defaults: new { controller = "cart", action = "addItem", id = UrlParameter.Optional },
               namespaces: new[] { "DoAnWebSell.Controllers" }
           );

            routes.MapRoute(
               name: "Payment",
               url: "thanh-toan",
               defaults: new { controller = "cart", action = "payment", id = UrlParameter.Optional },
               namespaces: new[] { "DoAnWebSell.Controllers" }
           );

            routes.MapRoute(
               name: "Payment Success",
               url: "hoan-thanh",
               defaults: new { controller = "cart", action = "success", id = UrlParameter.Optional },
               namespaces: new[] { "DoAnWebSell.Controllers" }
           );

            routes.MapRoute(
               name: "Search",
               url: "tim-kiem",
               defaults: new { controller = "product", action = "search", id = UrlParameter.Optional },
               namespaces: new[] { "DoAnWebSell.Controllers" }
           );

            routes.MapRoute(
              name: "Register",
              url: "dang-ky",
              defaults: new { controller = "user", action = "register", id = UrlParameter.Optional },
              namespaces: new[] { "DoAnWebSell.Controllers" }
          );

            routes.MapRoute(
              name: "Login user",
              url: "dang-nhap",
              defaults: new { controller = "user", action = "login", id = UrlParameter.Optional },
              namespaces: new[] { "DoAnWebSell.Controllers" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "DoAnWebSell.Controllers" }
            );
        }
    }
}
